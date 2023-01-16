using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core.Configuration;
using Yi.Framework.WeChatPay.Extensions;
using Yi.Framework.WeChatPay.Options;

namespace Yi.Framework.WeChatPay
{
    public class YiFrameworkWeChatPayModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddWeChatPayServer(option =>
            {
                option = Appsettings.app<PayOptions>(nameof(PayOptions));
            });
        }
    }
}