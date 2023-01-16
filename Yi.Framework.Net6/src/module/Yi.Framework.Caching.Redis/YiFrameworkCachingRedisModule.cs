using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core.Configuration;

namespace Yi.Framework.Caching.Redis
{

    public class YiFrameworkCachingRedisModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.Configure<CachingConnOptions>(Appsettings.appConfiguration("CachingConnOptions"));
            services.AddSingleton<CacheManager, RedisCacheClient>();
        }
    }

}