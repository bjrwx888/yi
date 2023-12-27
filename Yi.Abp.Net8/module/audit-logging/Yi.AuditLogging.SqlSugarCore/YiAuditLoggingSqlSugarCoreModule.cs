using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;
using Yi.Framework.SqlSugarCore;

namespace Yi.AuditLogging.SqlSugarCore
{
    [DependsOn(typeof(AbpAuditLoggingDomainModule))]
    [DependsOn(typeof(YiFrameworkSqlSugarCoreModule))]
    public class YiAuditLoggingSqlSugarCoreModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddYiDbContext<YiAuditLoggingDbContext>();
        }
    }
}
