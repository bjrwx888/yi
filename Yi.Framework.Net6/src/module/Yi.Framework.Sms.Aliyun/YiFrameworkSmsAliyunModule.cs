using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;

namespace Yi.Framework.Sms.Aliyun
{
    public class YiFrameworkSmsAliyunModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {

        }
    }
}