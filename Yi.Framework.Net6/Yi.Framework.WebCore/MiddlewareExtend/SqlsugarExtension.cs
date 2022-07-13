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
        public static void AddSqlsugarServer(this IServiceCollection services, Action<SqlSugarClient> action = null)
        {

           

            DbType dbType;
            var slavaConFig = new List<SlaveConnectionConfig>();
            if (Appsettings.appBool("MutiDB_Enabled"))
            {
                var readCon = Appsettings.app<List<string>>("DbConn", "ReadUrl");
              
                readCon.ForEach(s => {
                    slavaConFig.Add(new SlaveConnectionConfig() { ConnectionString = s });
                });
            }
           
            switch (Appsettings.app("DbSelect"))
            {
                case "Mysql": dbType = DbType.MySql; break;
                case "Sqlite": dbType = DbType.Sqlite; break;
                case "Sqlserver": dbType = DbType.SqlServer; break;
                case "Oracle": dbType = DbType.Oracle; break;
                default:throw new Exception("DbSelect配置写的TM是个什么东西？");
            }
            SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
            {
                //准备添加分表分库
                DbType = dbType,
                ConnectionString = Appsettings.app("DbConn", "WriteUrl"),
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
                        // int?  decimal?这种 isnullable=true
                        if (c.PropertyType.IsGenericType &&
                        c.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            p.IsNullable = true;
                        }
                    }
                }
            },
         db =>
         {
             if (action.IsNotNull())
             {
                 action(db);
             }

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
                 foreach (var i in p)
                 {
                     Console.WriteLine("参数:" +i.ParameterName+",参数值"+i.Value);
                 }
                 Console.WriteLine("_______________________________________________");
             };

         });
            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
        }
    }

}