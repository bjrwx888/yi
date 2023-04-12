using SqlSugar;

namespace Yi.Furion.Rbac.Application;

public class SystemService : ISystemService, ITransient
{
    private readonly ISqlSugarClient _sqlSugarClient;
    public SystemService(ISqlSugarClient sqlSugarClient)
    {
        _sqlSugarClient=sqlSugarClient;
    }
    public string GetDescription()
    {
        return "让 .NET 开发更简单，更通用，更流行。";
    }
}
