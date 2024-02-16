using Yi.Framework.CodeGun.Application.Contracts;
using Yi.Framework.CodeGun.Domain;
using Yi.Framework.Ddd.Application;

namespace Yi.Framework.CodeGun.Application
{
    [DependsOn(typeof(YiFrameworkCodeGunApplicationContractsModule),
        typeof(YiFrameworkCodeGunDomainModule),
        typeof(YiFrameworkDddApplicationModule))]
    public class YiFrameworkCodeGunApplicationModule : AbpModule
    {

    }
}
