using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.DigitalCollectibles.Domain.Shared;
using Yi.Framework.AuditLogging.Domain;
using Yi.Framework.Bbs.Domain;
using Yi.Framework.ChatHub.Domain;
using Yi.Framework.Mapster;
using Yi.Framework.Rbac.Domain;
using Yi.Framework.TenantManagement.Domain;

namespace Yi.Framework.DigitalCollectibles.Domain
{
    [DependsOn(
        typeof(YiFrameworkDigitalCollectiblesDomainSharedModule),
       
        typeof(YiFrameworkTenantManagementDomainModule),
        typeof(YiFrameworkRbacDomainModule),
        typeof(YiFrameworkBbsDomainModule),
        typeof(YiFrameworkChatHubDomainModule),
        typeof(YiFrameworkAuditLoggingDomainModule),

        typeof(YiFrameworkMapsterModule),
        typeof(AbpDddDomainModule),
        typeof(AbpCachingModule)
        )]
    public class YiFrameworkDigitalCollectiblesDomainModule : AbpModule
    {

    }
}