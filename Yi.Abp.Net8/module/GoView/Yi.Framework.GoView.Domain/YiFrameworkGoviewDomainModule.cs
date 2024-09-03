using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.GoView.Domain.Shared;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.GoView.Domain
{
    [DependsOn(typeof(AbpDddDomainModule),
        typeof(YiFrameworkGoViewDomainSharedModule),
        typeof(YiFrameworkSqlSugarCoreAbstractionsModule))]
    public class YiFrameworkGoviewDomainModule : AbpModule
    {
    }
}
