using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Yi.Framework.Ddd.Application.Contracts;

namespace Yi.Framework.Ddd.Application
{
    [DependsOn(typeof(AbpDddApplicationModule),
        typeof(YiFrameworkDddApplicationContractsModule))]
    public class YiFrameworkDddApplicationModule : AbpModule
    {

    }
}
