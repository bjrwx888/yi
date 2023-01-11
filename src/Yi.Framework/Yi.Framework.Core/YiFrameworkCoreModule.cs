using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Module;

namespace Yi.Framework.Core
{
    public class YiFrameworkCoreModule : IYiModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            throw new NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            throw new NotImplementedException();
        }
    }
}
