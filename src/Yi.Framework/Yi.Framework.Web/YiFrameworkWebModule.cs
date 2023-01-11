using StartupModules;
using Yi.Framework.Core.Module;

namespace Yi.Framework.Web
{
    public class YiFrameworkWebModule : IYiModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
       
        }
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            Console.WriteLine("还有谁");
 
        }
    }
}
