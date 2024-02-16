using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Yi.Framework.CodeGun.Domain.Shared
{
    [DependsOn(typeof(AbpDddDomainSharedModule))]
    public class YiFrameworkCodeGunDomainSharedModule:AbpModule
    {

    }
}
