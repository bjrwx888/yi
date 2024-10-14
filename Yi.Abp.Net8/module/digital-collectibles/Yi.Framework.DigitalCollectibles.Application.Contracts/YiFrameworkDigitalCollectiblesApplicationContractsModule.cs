using Yi.Framework.DigitalCollectibles.Domain.Shared;
using Yi.Framework.Ddd.Application.Contracts;

namespace Yi.Framework.DigitalCollectibles.Application.Contracts
{
    [DependsOn(
        typeof(YiFrameworkDigitalCollectiblesDomainSharedModule),
        
        typeof(YiFrameworkDddApplicationContractsModule))]
    public class YiFrameworkDigitalCollectiblesApplicationContractsModule:AbpModule
    {

    }
}