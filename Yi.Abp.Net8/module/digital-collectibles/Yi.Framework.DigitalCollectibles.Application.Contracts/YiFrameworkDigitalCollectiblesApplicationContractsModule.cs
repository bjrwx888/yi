using Yi.Framework.DigitalCollectibles.Domain.Shared;
using Yi.Framework.Bbs.Application.Contracts;
using Yi.Framework.ChatHub.Application.Contracts;
using Yi.Framework.Ddd.Application.Contracts;
using Yi.Framework.Rbac.Application.Contracts;
using Yi.Framework.TenantManagement.Application.Contracts;

namespace Yi.Framework.DigitalCollectibles.Application.Contracts
{
    [DependsOn(
        typeof(YiFrameworkDigitalCollectiblesDomainSharedModule),

        typeof(YiFrameworkRbacApplicationContractsModule),
        typeof(YiFrameworkBbsApplicationContractsModule),
        typeof(YiFrameworkChatHubApplicationContractsModule),

        typeof(YiFrameworkTenantManagementApplicationContractsModule),
        typeof(YiFrameworkDddApplicationContractsModule))]
    public class YiFrameworkDigitalCollectiblesApplicationContractsModule:AbpModule
    {

    }
}