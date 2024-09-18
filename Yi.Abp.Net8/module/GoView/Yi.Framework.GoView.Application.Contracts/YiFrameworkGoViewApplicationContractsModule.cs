using Volo.Abp.Modularity;
using Yi.Framework.Ddd.Application.Contracts;
using Yi.Framework.GoView.Domain.Shared;

namespace Yi.Framework.GoView.Application.Contracts
{
    [DependsOn(typeof(YiFrameworkGoViewDomainSharedModule),
        typeof(YiFrameworkDddApplicationContractsModule))]
    public class YiFrameworkGoViewApplicationContractsModule : AbpModule
    {
    }
}
