using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Sqlsugar.Options;

namespace Yi.Framework.Core.Sqlsugar.Extensions
{
    public static class SqlsugarCodeFirstExtensions
    {
        public static void UseSqlsugarCodeFirstServer(this IApplicationBuilder app)
        {
            var db = app.ApplicationServices.GetRequiredService<ISqlSugarClient>();
            var options = app.ApplicationServices.GetRequiredService<IOptions<DbConnOptions>>();

            if (options.Value.EnabledCodeFirst == false) return;

            var assemblys = new List<Assembly>();

            //全盘加载
            if (options.Value.EntityAssembly is null)
            {
                assemblys.AddRange(AppDomain.CurrentDomain.GetAssemblies().ToList());
            }
            //按需加载
            else
            {
                options.Value.EntityAssembly.ForEach(a =>
                {
                    assemblys.Add(Assembly.Load(a));
                });
            }

            foreach (var assembly in assemblys)
            {
                TableInvoer(db, assembly.GetTypes().ToList());
            }

        }

        private static void TableInvoer(ISqlSugarClient _Db, List<Type> typeList)
        {
            _Db.DbMaintenance.CreateDatabase();
            foreach (var t in typeList)
            {
                //扫描如果存在SugarTable特性 并且 不是分表模型，直接codefirst
                if (t.GetCustomAttributes(false).Any(a => a.GetType().Equals(typeof(SugarTable))
                && !t.GetCustomAttributes(false).Any(a => a.GetType().Equals(typeof(SplitTableAttribute)))))
                {
                    _Db.CodeFirst.SetStringDefaultLength(200).InitTables(t);//这样一个表就能成功创建了
                }
            }
        }
    }
}
