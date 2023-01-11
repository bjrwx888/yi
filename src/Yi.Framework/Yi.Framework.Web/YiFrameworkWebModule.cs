using StartupModules;

namespace Yi.Framework.Web
{
    public class YiFrameworkWebModule : IStartupModule
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
