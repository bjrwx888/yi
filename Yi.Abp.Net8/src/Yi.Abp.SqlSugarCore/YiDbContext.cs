using Microsoft.Extensions.Logging;
using SqlSugar;
using Volo.Abp.DependencyInjection;
using Yi.Framework.Rbac.SqlSugarCore;

namespace Yi.Abp.SqlSugarCore
{
    public class YiDbContext : YiRbacDbContext
    {
        public YiDbContext(IAbpLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
        {
        }

        protected override void CustomDataFilter()
        {
            base.CustomDataFilter();
        }

        protected override void DataExecuted(object oldValue, DataAfterModel entityInfo)
        {
            base.DataExecuted(oldValue, entityInfo);
        }

        protected override void DataExecuting(object oldValue, DataFilterModel entityInfo)
        {
            base.DataExecuting(oldValue, entityInfo);
        }

        protected override void OnLogExecuting(string sql, SugarParameter[] pars)
        {
            //获取原生SQL推荐 5.1.4.63  性能OK
            //UtilMethods.GetNativeSql(sql,pars)

            //获取无参数化SQL 影响性能只适合调试
            this.Logger.CreateLogger<YiDbContext>().LogInformation(UtilMethods.GetSqlString(DbType.SqlServer, sql, pars)); 
        }

        protected override void OnLogExecuted(string sql, SugarParameter[] pars)
        {
            base.OnLogExecuted(sql, pars);
        }

        protected override void OnSqlSugarClientConfig(ISqlSugarClient sqlSugarClient)
        {
            base.OnSqlSugarClientConfig(sqlSugarClient);
        }
    }
}
