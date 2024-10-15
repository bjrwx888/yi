using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.DigitalCollectibles.Domain.Shared;
using Yi.Framework.Mapster;
using Yi.Framework.SettingManagement.Domain;

namespace Yi.Framework.DigitalCollectibles.Domain
{
    [DependsOn(
        typeof(YiFrameworkDigitalCollectiblesDomainSharedModule),

        typeof(YiFrameworkSettingManagementDomainModule),
        typeof(YiFrameworkMapsterModule),
        typeof(AbpDddDomainModule),
        typeof(AbpCachingModule)
        )]
    public class YiFrameworkDigitalCollectiblesDomainModule : AbpModule
    {

    }
}