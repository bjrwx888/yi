using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;

namespace Yi.Framework.OperLog
{
    [DependsOn(typeof(YiFrameworkCoreModule))]
    public class YiFrameworkOperLogModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddControllers(options => {
                options.Filters.Add<GlobalOperLogAttribute>();
            });
            services.AddSingleton<GlobalOperLogAttribute>();
        }
    }
}