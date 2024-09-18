using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Yi.Framework.GoView.Domain.Shared
{
    [DependsOn(typeof(AbpDddDomainSharedModule))]
    public class YiFrameworkGoViewDomainSharedModule : AbpModule
    {
    }
}
