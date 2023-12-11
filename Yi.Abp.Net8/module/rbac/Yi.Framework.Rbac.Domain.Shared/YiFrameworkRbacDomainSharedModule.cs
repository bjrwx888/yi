using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.Mapster;

namespace Yi.Framework.Rbac.Domain.Shared
{
    [DependsOn(typeof(AbpDddDomainSharedModule),
        typeof(YiFrameworkMapsterModule)
        )]
    public class YiFrameworkRbacDomainSharedModule : AbpModule
    {

    }
}