using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.AspNetCore.CurrentUser;
using Yi.Framework.Core.CurrentUsers.Accessor;
using Yi.Framework.Core.CurrentUsers;

namespace Yi.Framework.AspNetCore
{
    public class YiFrameworkAspNetCoreModule : IStartupModule
    {

        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddHttpContextAccessor();
            services.AddCurrentUserServer();

        }
    }
}
