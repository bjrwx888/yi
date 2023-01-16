using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Caching.MemoryCache;
using Yi.Framework.Caching;

namespace Yi.Framework.Ddd
{
    public class YiFrameworkCachingMemoryCacheModule:IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddSingleton<CacheManager, MemoryCacheClient>();
        }
    }
}
