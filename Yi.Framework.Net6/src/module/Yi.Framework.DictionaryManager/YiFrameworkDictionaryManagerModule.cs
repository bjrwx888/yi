using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;

namespace Yi.Framework.DictionaryManager
{
    [DependsOn(typeof(YiFrameworkCoreModule))]
    public class YiFrameworkDictionaryManagerModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
        }
    }
}