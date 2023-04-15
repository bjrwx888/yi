using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.Configuration;

namespace Yi.Framework.Sms.Aliyun
{
    [DependsOn(typeof(YiFrameworkCoreModule))]
    public class YiFrameworkSmsAliyunModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.Configure<SmsAliyunOptions>(Appsettings.appConfiguration("SmsAliyunOptions"));
            services.AddSingleton<SmsAliyunManager>();
        }
    }
}