using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Yi.Framework.DigitalCollectibles.Domain;
using Yi.Framework.AuditLogging.SqlSugarCore;
using Yi.Framework.Bbs.SqlSugarCore;
using Yi.Framework.ChatHub.SqlSugarCore;
using Yi.Framework.CodeGen.SqlSugarCore;
using Yi.Framework.Mapster;
using Yi.Framework.Rbac.SqlSugarCore;
using Yi.Framework.SqlSugarCore;
using Yi.Framework.SqlSugarCore.Abstractions;
using Yi.Framework.TenantManagement.SqlSugarCore;

namespace Yi.Framework.DigitalCollectibles.SqlsugarCore
{
    [DependsOn(
        typeof(YiFrameworkDigitalCollectiblesDomainModule),

        typeof(YiFrameworkRbacSqlSugarCoreModule),
        typeof(YiFrameworkBbsSqlSugarCoreModule),
        typeof(YiFrameworkCodeGenSqlSugarCoreModule),
         typeof(YiFrameworkChatHubSqlSugarCoreModule),

        typeof(YiFrameworkAuditLoggingSqlSugarCoreModule),
        typeof(YiFrameworkTenantManagementSqlSugarCoreModule),
        typeof(YiFrameworkMapsterModule),
        typeof(YiFrameworkSqlSugarCoreModule)
        )]
    public class YiFrameworkDigitalCollectiblesSqlSugarCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}