using System.Reflection;
using SqlSugar;
using Volo.Abp.DependencyInjection;

namespace Yi.Framework.SqlSugarCore;

public abstract class SqlSugarDbContext : ISqlSugarDbContextDependencies
{
    //属性注入
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }
    protected ISqlSugarClient SqlSugarClient { get;private set; }
    public int ExecutionOrder => 0;

    public void OnSqlSugarClientConfig(ISqlSugarClient sqlSugarClient)
    {
        SqlSugarClient = sqlSugarClient;
        CustomDataFilter(sqlSugarClient);
    }
    protected virtual void CustomDataFilter(ISqlSugarClient sqlSugarClient)
    {
    }
    
    public virtual void DataExecuted(object oldValue, DataAfterModel entityInfo)
    {
    }

    public virtual void DataExecuting(object oldValue, DataFilterModel entityInfo)
    {
    }

    public virtual void OnLogExecuting(string sql, SugarParameter[] pars)
    {
    }

    public virtual void OnLogExecuted(string sql, SugarParameter[] pars)
    {
    }

    public virtual void EntityService(PropertyInfo propertyInfo, EntityColumnInfo entityColumnInfo)
    {
    }
}