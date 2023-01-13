using Volo.Abp;
using Volo.Abp.Modularity;
using Yi.Framework.Domain.Shared;

namespace Yi.Framework.Application.Contracts
{
    [DependsOn(
        typeof(YiFrameworkDomainSharedModule))]
    public class YiFrameworkApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
        }


    }
}
