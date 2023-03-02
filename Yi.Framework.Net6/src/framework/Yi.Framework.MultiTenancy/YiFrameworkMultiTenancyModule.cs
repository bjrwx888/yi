using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.MultiTenancy.Extensions;

namespace Yi.Framework.MultiTenancy
{
    public class YiFrameworkMultiTenancyModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddCurrentTenant();
        }
    }
}