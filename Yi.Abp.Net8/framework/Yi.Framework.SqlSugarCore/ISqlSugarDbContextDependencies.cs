using System.Reflection;
using SqlSugar;

namespace Yi.Framework.SqlSugarCore;

public interface ISqlSugarDbContextDependencies
{
    void OnSqlSugarClientConfig(ISqlSugarClient sqlSugarClient);
    void DataExecuted(object oldValue, DataAfterModel entityInfo);
    void DataExecuting(object oldValue, DataFilterModel entityInfo);

    void OnLogExecuting(string sql, SugarParameter[] pars);
    void OnLogExecuted(string sql, SugarParameter[] pars);
    
    void EntityService(PropertyInfo propertyInfo, EntityColumnInfo entityColumnInfo);
}