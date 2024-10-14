using Yi.Framework.DigitalCollectibles.Application.Contracts;
using Yi.Framework.DigitalCollectibles.Domain;
using Yi.Framework.Bbs.Application;
using Yi.Framework.ChatHub.Application;
using Yi.Framework.CodeGen.Application;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Application;
using Yi.Framework.TenantManagement.Application;

namespace Yi.Framework.DigitalCollectibles.Application
{
    [DependsOn(
        typeof(YiFrameworkDigitalCollectiblesApplicationContractsModule),
        typeof(YiFrameworkDigitalCollectiblesDomainModule),


        typeof(YiFrameworkRbacApplicationModule),
         typeof(YiFrameworkBbsApplicationModule),
         typeof(YiFrameworkChatHubApplicationModule),
        typeof(YiFrameworkTenantManagementApplicationModule),
        typeof(YiFrameworkCodeGenApplicationModule),

        typeof(YiFrameworkDddApplicationModule)
        )]
    public class YiFrameworkDigitalCollectiblesApplicationModule : AbpModule
    {
    }
}
