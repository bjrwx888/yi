﻿using System.Collections;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;
using Yi.Framework.SqlSugarCore.Abstractions;
using Check = Volo.Abp.Check;

namespace Yi.Framework.SqlSugarCore
{
    public class SqlSugarDbContext : ISqlSugarDbContext
    {
        /// <summary>
        /// SqlSugar 客户端
        /// </summary>
        public ISqlSugarClient SqlSugarClient { get; private set; }

        protected ICurrentUser CurrentUser => LazyServiceProvider.GetRequiredService<ICurrentUser>();
        private IAbpLazyServiceProvider LazyServiceProvider { get; }

        private IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetRequiredService<IGuidGenerator>();
        private ILoggerFactory Logger => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();
        private ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();
        protected IDataFilter DataFilter => LazyServiceProvider.LazyGetRequiredService<IDataFilter>();
        protected virtual bool IsMultiTenantFilterEnabled => DataFilter?.IsEnabled<IMultiTenant>() ?? false;

        protected virtual bool IsSoftDeleteFilterEnabled => DataFilter?.IsEnabled<ISoftDelete>() ?? false;

        private IEntityChangeEventHelper EntityChangeEventHelper =>
            LazyServiceProvider.LazyGetService<IEntityChangeEventHelper>(NullEntityChangeEventHelper.Instance);

        public DbConnOptions Options => LazyServiceProvider.LazyGetRequiredService<IOptions<DbConnOptions>>().Value;

        private ISerializeService SerializeService => LazyServiceProvider.LazyGetRequiredService<ISerializeService>();

        private IEnumerable<ISqlSugarDbContextDependencies> SqlSugarDbContextDependencies=>LazyServiceProvider.LazyGetRequiredService<IEnumerable<ISqlSugarDbContextDependencies>>();
        public void SetSqlSugarClient(ISqlSugarClient sqlSugarClient)
        {
            SqlSugarClient = sqlSugarClient;
        }

        public SqlSugarDbContext(IAbpLazyServiceProvider lazyServiceProvider)
        {
            LazyServiceProvider = lazyServiceProvider;
            var connectionCreators = LazyServiceProvider.LazyGetRequiredService<ISqlSugarDbContextDependencies>();

            var connectionConfig = BuildConnectionConfig(action: options =>
                 {
                     options.ConnectionString = GetCurrentConnectionString();
                     options.DbType = GetCurrentDbType();
                 });
            SqlSugarClient = new SqlSugarClient(connectionConfig);
            //替换默认序列化器
            SqlSugarClient.CurrentConnectionConfig.ConfigureExternalServices.SerializeService = SerializeService;
           
            //最后一步，将全程序dbcontext汇总
            // Aop及多租户连接字符串和类型，需要单独设置
            SetDbAop(SqlSugarClient);
        }

        /// <summary>
        /// 构建Aop-sqlsugaraop在多租户模式中，需单独设置
        /// </summary>
        /// <param name="sqlSugarClient"></param>
        public void SetDbAop(ISqlSugarClient sqlSugarClient)
        {
            //将所有，ISqlSugarDbContextDependencies进行累加
            sqlSugarClient.Aop.OnLogExecuting = this.OnLogExecuting;
            sqlSugarClient.Aop.OnLogExecuted = this.OnLogExecuted;
            sqlSugarClient.Aop.DataExecuting = this.DataExecuting;
            sqlSugarClient.Aop.DataExecuted = this.DataExecuted;
            OnSqlSugarClientConfig(currentDb);
        }

        /// <summary>
        /// 构建连接配置
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ConnectionConfig BuildConnectionConfig(Action<ConnectionConfig>? action=null)
        {
            var dbConnOptions = Options;
            #region 组装options
            if (dbConnOptions.DbType is null)
            {
                throw new ArgumentException("DbType配置为空");
            }
            var slavaConFig = new List<SlaveConnectionConfig>();
            if (dbConnOptions.EnabledReadWrite)
            {
                if (dbConnOptions.ReadUrl is null)
                {
                    throw new ArgumentException("读写分离为空");
                }

                var readCon = dbConnOptions.ReadUrl;

                readCon.ForEach(s =>
                {
                    //如果是动态saas分库，这里的连接串都不能写死，需要动态添加，这里只配置共享库的连接
                    slavaConFig.Add(new SlaveConnectionConfig() { ConnectionString = s });
                });
            }
            #endregion

            #region 组装连接config
            var connectionConfig = new ConnectionConfig()
            {
                ConfigId= ConnectionStrings.DefaultConnectionStringName,
                DbType = dbConnOptions.DbType ?? DbType.Sqlite,
                ConnectionString = dbConnOptions.Url,
                IsAutoCloseConnection = true,
                SlaveConnectionConfigs = slavaConFig,
                //设置codefirst非空值判断
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    // 处理表
                    EntityNameService = (type, entity) =>
                    {
                        if (dbConnOptions.EnableUnderLine && !entity.DbTableName.Contains('_'))
                            entity.DbTableName = UtilMethods.ToUnderLine(entity.DbTableName);// 驼峰转下划线
                    },
                    EntityService = (c, p) =>
                    {
                        if (new NullabilityInfoContext()
                        .Create(c).WriteState is NullabilityState.Nullable)
                        {
                            p.IsNullable = true;
                        }

                        if (dbConnOptions.EnableUnderLine && !p.IsIgnore && !p.DbColumnName.Contains('_'))
                            p.DbColumnName = UtilMethods.ToUnderLine(p.DbColumnName);// 驼峰转下划线
                       
                        //将所有，ISqlSugarDbContextDependencies的EntityService进行累加
                        //额外的实体服务需要这里配置，
                        EntityService(c, p);
                    }
                },
                //这里多租户有个坑，这里配置是无效的
                // AopEvents = new AopEvents
                // {
                //     DataExecuted = DataExecuted,
                //     DataExecuting = DataExecuting,
                //     OnLogExecuted = OnLogExecuted,
                //     OnLogExecuting = OnLogExecuting
                // }
            };

            if (action is not null)
            {
                action.Invoke(connectionConfig);
            }
            #endregion
            return connectionConfig;
        }
        
        /// <summary>
        /// db切换多库支持
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCurrentConnectionString()
        {
            var connectionStringResolver = LazyServiceProvider.LazyGetRequiredService<IConnectionStringResolver>();
            var connectionString =
                connectionStringResolver.ResolveAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Check.NotNull(Options.Url, "dbUrl未配置");
            }

            return connectionString!;
        }

        protected virtual DbType GetCurrentDbType()
        {
            if (CurrentTenant.Name is not null)
            {
                var dbTypeFromTenantName = GetDbTypeFromTenantName(CurrentTenant.Name);
                if (dbTypeFromTenantName is not null)
                {
                    return dbTypeFromTenantName.Value;
                }
            }

            Check.NotNull(Options.DbType, "默认DbType未配置！");
            return Options.DbType!.Value;
        }

        //根据租户name进行匹配db类型:  Test_Sqlite，[来自AI]
        private DbType? GetDbTypeFromTenantName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            // 查找下划线的位置
            int underscoreIndex = name.LastIndexOf('_');

            if (underscoreIndex == -1 || underscoreIndex == name.Length - 1)
            {
                return null;
            }

            // 提取 枚举 部分
            string enumString = name.Substring(underscoreIndex + 1);

            // 尝试将 尾缀 转换为枚举
            if (Enum.TryParse<DbType>(enumString, out DbType result))
            {
                return result;
            }

            // 条件不满足时返回 null
            return null;
        }
        

        public void BackupDataBase()
        {
            string directoryName = "database_backup";
            string fileName = DateTime.Now.ToString($"yyyyMMdd_HHmmss") + $"_{SqlSugarClient.Ado.Connection.Database}";
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            switch (Options.DbType)
            {
                case DbType.MySql:
                    //MySql
                    SqlSugarClient.DbMaintenance.BackupDataBase(SqlSugarClient.Ado.Connection.Database,
                        $"{Path.Combine(directoryName, fileName)}.sql"); //mysql 只支持.net core
                    break;


                case DbType.Sqlite:
                    //Sqlite
                    SqlSugarClient.DbMaintenance.BackupDataBase(null, $"{fileName}.db"); //sqlite 只支持.net core
                    break;


                case DbType.SqlServer:
                    //SqlServer
                    SqlSugarClient.DbMaintenance.BackupDataBase(SqlSugarClient.Ado.Connection.Database,
                        $"{Path.Combine(directoryName, fileName)}.bak" /*服务器路径*/); //第一个参数库名 
                    break;


                default:
                    throw new NotImplementedException("其他数据库备份未实现");
            }
        }
    }
}