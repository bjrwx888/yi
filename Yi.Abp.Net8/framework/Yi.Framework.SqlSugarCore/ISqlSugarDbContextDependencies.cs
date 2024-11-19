using System.Reflection;
using SqlSugar;

namespace Yi.Framework.SqlSugarCore;

public interface ISqlSugarDbContextDependencies
{
    void OnSqlSugarClientConfig(ISqlSugarClient client);

    void DataExecuted(object obj, DataAfterModel dataAfterModel);
    void DataExecuting(object obj, DataFilterModel dataAfterModel);

    void OnLogExecuting(string str, SugarParameter[] parameters);
    void OnLogExecuted(string str, SugarParameter[] parameters);
    
    void EntityService(PropertyInfo propertyInfo, EntityColumnInfo entityColumnInfo);
}