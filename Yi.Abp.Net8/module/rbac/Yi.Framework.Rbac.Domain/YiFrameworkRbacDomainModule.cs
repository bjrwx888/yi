using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.Mapster;
using Yi.Framework.Rbac.Domain.Authorization;
using Yi.Framework.Rbac.Domain.Operlog;
using Yi.Framework.Rbac.Domain.Shared;

namespace Yi.Framework.Rbac.Domain
{
    [DependsOn(
        typeof(YiFrameworkRbacDomainSharedModule),

        typeof(AbpAspNetCoreSignalRModule),
        typeof(AbpDddDomainModule),
        typeof(AbpCachingModule)
        )]
    public class YiFrameworkRbacDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var service = context.Services;
            service.AddControllers(options =>
            {
                options.Filters.Add<PermissionGlobalAttribute>();
                options.Filters.Add<OperLogGlobalAttribute>();
            });
        }
    }
}