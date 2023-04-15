using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Data.Entities;

namespace Yi.Framework.Infrastructure.Sqlsugar
{
    public class SqlSugarDbContext
    {
        /// <summary>
        /// SqlSugar 客户端
        /// </summary>
        public ISqlSugarClient SqlSugarClient { get; set; }

        protected ICurrentUser _currentUser;

        protected ILogger<SqlSugarDbContext> _logger;

        protected IOptions<DbConnOptions> _options;

        public SqlSugarDbContext(IOptions<DbConnOptions> options, ICurrentUser currentUser, ILogger<SqlSugarDbContext> logger)
        {
            _currentUser = currentUser;
            _logger = logger;
            _options = options;
            var dbConnOptions = options.Value;
            #region 组装options
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
            #endregion
            SqlSugarClient = new SqlSugarScope(new ConnectionConfig()
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

             db.Aop.DataExecuting = (oldValue, entityInfo) =>
             {

                 switch (entityInfo.OperationType)
                 {
                     case DataFilterType.UpdateByObject:

                         if (entityInfo.PropertyName.Equals(nameof(IAuditedObject.LastModificationTime)))
                         {
                             entityInfo.SetValue(DateTime.Now);
                         }
                         if (entityInfo.PropertyName.Equals(nameof(IAuditedObject.LastModifierId)))
                         {
                             if (_currentUser != null)
                             {
                                 entityInfo.SetValue(_currentUser.Id);
                             }
                         }
                         break;
                     case DataFilterType.InsertByObject:
                         if (entityInfo.PropertyName.Equals(nameof(IAuditedObject.CreationTime)))
                         {
                             entityInfo.SetValue(DateTime.Now);
                         }
                         if (entityInfo.PropertyName.Equals(nameof(IAuditedObject.CreatorId)))
                         {
                             if (_currentUser != null)
                             {
                                 entityInfo.SetValue(_currentUser.Id);
                             }
                         }

                         //插入时，需要租户id,先预留
                         if (entityInfo.PropertyName.Equals(nameof(IMultiTenant.TenantId)))
                         {
                             //if (this.CurrentTenant is not null)
                             //{
                             //    entityInfo.SetValue(this.CurrentTenant.Id);
                             //}
                         }
                         break;
                 }
             };
             db.Aop.OnLogExecuting = (s, p) =>
             {
                 StringBuilder sb = new StringBuilder();
                 //sb.Append("执行SQL:" + s.ToString());
                 //foreach (var i in p)
                 //{
                 //    sb.Append($"\r\n参数:{i.ParameterName},参数值:{i.Value}");
                 //}
                 sb.Append($"\r\n 完整SQL：{UtilMethods.GetSqlString(DbType.MySql, s, p)}");
                 logger?.LogDebug(sb.ToString());
             };
             //扩展
             OnSqlSugarClientConfig(db);
         });
        }

        //上下文对象扩展
        protected virtual void OnSqlSugarClientConfig(ISqlSugarClient sqlSugarClient)
        {
        }
    }
}
