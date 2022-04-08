using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.WebCore.MiddlewareExtend
{
    public static class SqlsugarExtension
    {
        public static void AddSqlsugarServer(this IServiceCollection services)
        {
            SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
            {
                DbType = SqlSugar.DbType.MySql,
                ConnectionString = Appsettings.app("DbConn", "WriteUrl"),
                IsAutoCloseConnection = true
            },
         db =>
         {

             db.Aop.DataExecuting = (oldValue, entityInfo) =>
             {
                 //var httpcontext = ServiceLocator.Instance.GetService<IHttpContextAccessor>().HttpContext;
                 switch (entityInfo.OperationType)
                 {
                     case DataFilterType.InsertByObject:
                         if (entityInfo.PropertyName == "CreateUser")
                         {
                             //entityInfo.SetValue(new Guid(httpcontext.Request.Headers["Id"].ToString()));
                         }

                         if (entityInfo.PropertyName == "TenantId")
                         {
                             //entityInfo.SetValue(new Guid(httpcontext.Request.Headers["TenantId"].ToString()));
                         }
                         break;
                     case DataFilterType.UpdateByObject:
                         if (entityInfo.PropertyName == "ModifyTime")
                         {
                             entityInfo.SetValue(DateTime.Now);
                         }
                         if (entityInfo.PropertyName == "ModifyUser")
                         {
                             //entityInfo.SetValue(new Guid(httpcontext.Request.Headers["Id"].ToString()));
                         }
                         break;
                 }

             };
             db.Aop.OnLogExecuting = (s, p) =>
             {

                 Console.WriteLine("_______________________________________________");
                 Console.WriteLine("执行SQL:"+s.ToString());
                 Console.WriteLine("_______________________________________________");
             };

         });
            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
        }
    }

}