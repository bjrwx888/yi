using Yi.Framework.CodeGun.Domain.Shared;
using Yi.Framework.Ddd.Application.Contracts;

namespace Yi.Framework.CodeGun.Application.Contracts
{
    [DependsOn(typeof(YiFrameworkCodeGunDomainSharedModule),
        typeof(YiFrameworkDddApplicationContractsModule))]
    public class YiFrameworkCodeGunApplicationContractsModule:AbpModule
    {

    }
}
