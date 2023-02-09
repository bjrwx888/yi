using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.AspNetCore
{
    public class YiFrameworkAspNetCoreModule : IStartupModule
    {

        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            app.UseCurrentUserServer();
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddCurrentUserServer();
        }
    }
}
