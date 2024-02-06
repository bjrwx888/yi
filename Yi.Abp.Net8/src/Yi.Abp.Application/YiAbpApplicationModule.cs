using Volo.Abp.Modularity;
using Yi.Abp.Application.Contracts;
using Yi.Abp.Domain;
using Yi.Framework.Bbs.Application;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Application;
using Yi.Framework.TenantManagement.Application;

namespace Yi.Abp.Application
{
    [DependsOn(
        typeof(YiAbpApplicationContractsModule),
        typeof(YiAbpDomainModule),
       

        typeof(YiFrameworkRbacApplicationModule),
         typeof(YiFrameworkBbsApplicationModule),
        typeof(YiFrameworkTenantManagementApplicationModule),


        typeof(YiFrameworkDddApplicationModule)
        )]
    public class YiAbpApplicationModule : AbpModule
    {
    }
}
