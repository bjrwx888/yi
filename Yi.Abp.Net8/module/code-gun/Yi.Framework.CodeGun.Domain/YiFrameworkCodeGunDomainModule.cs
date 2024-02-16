using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.CodeGun.Domain.Shared;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.CodeGun.Domain
{
    [DependsOn(typeof(YiFrameworkCodeGunDomainSharedModule),
        typeof(AbpDddDomainModule),
        typeof(YiFrameworkSqlSugarCoreAbstractionsModule))]
    public class YiFrameworkCodeGunDomainModule : AbpModule
    {

    }
}
