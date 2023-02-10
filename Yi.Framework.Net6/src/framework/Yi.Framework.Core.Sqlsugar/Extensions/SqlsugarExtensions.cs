using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Configuration;
using Yi.Framework.Core.Model;
using Yi.Framework.Core.Sqlsugar.Const;
using Yi.Framework.Core.Sqlsugar.Options;
using DbType = SqlSugar.DbType;

namespace Yi.Framework.Core.Sqlsugar.Extensions
{
    public static class SqlsugarExtensions
    {
        public static void AddSqlsugarServer(this IServiceCollection services, Action<SqlSugarClient>? action = null)
        {
            var dbConnOptions = Appsettings.app<DbConnOptions>("DbConnOptions");

            if (dbConnOptions.DbType is null)
            {
                throw new ArgumentException(SqlsugarConst.DbType配置为空);
            }
            var slavaConFig = new List<SlaveConnectionConfig>();



            if (dbConnOptions.EnabledReadWrite)
            {
                if (dbConnOptions.ReadUrl is null)
                {
                    throw new ArgumentException(SqlsugarConst.读写分离为空);
                }

                var readCon = dbConnOptions.ReadUrl;

                readCon.ForEach(s =>
                {
                    //如果是动态saas分库，这里的连接串都不能写死，需要动态添加，这里只配置共享库的连接
                    slavaConFig.Add(new SlaveConnectionConfig() { ConnectionString = s });
                });
            }




            SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
            {
                //准备添加分表分库
                DbType = dbConnOptions.DbType ?? DbType.Sqlite,
                ConnectionString = dbConnOptions.Url,
                IsAutoCloseConnection = true,
                MoreSettings = new ConnMoreSettings()
                {
                    DisableNvarchar = true
                },
                SlaveConnectionConfigs = slavaConFig,
                //设置codefirst非空值判断
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    EntityService = (c, p) =>
                    {
                        //高版C#写法 支持string?和string  
                        if (new NullabilityInfoContext()
                        .Create(c).WriteState is NullabilityState.Nullable)
                        {
                            p.IsNullable = true;
                        }
                    }
                }
            },
         db =>
         {
             //扩展
             if (action is not null)
             {
                 action(db);
             }
             db.Aop.DataExecuting = (oldValue, entityInfo) =>
             {

                 switch (entityInfo.OperationType)
                 {
                     case DataFilterType.InsertByObject:
                         break;
                     case DataFilterType.UpdateByObject:
                         break;
                 }

             };
             db.Aop.OnLogExecuting = (s, p) =>
             {
                 var _logger = ServiceLocatorModel.Instance?.GetRequiredService<ILogger<SqlSugarClient>>();

                 StringBuilder sb = new StringBuilder();
                 sb.Append("执行SQL:" + s.ToString());
                 foreach (var i in p)
                 {
                     sb.Append($"\r\n参数:{i.ParameterName},参数值:{i.Value}");
                 }
                 sb.Append($"\r\n 完整SQL：{UtilMethods.GetSqlString(DbType.MySql, s, p)}");
                 _logger?.LogDebug(sb.ToString());
             };

         });
            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
        }
    }
}
