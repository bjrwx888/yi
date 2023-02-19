using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core.Helper;

namespace Yi.Framework.EventBus
{
    public class YiFrameworkEventBusModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddCikeEventBus(opt =>
            {
                opt.AddHandlerForAsemmbly(AssemblyHelper.GetAllLoadAssembly());
            });
        }
    }
}