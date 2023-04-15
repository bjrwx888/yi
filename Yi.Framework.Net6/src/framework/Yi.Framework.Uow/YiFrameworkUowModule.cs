using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Ddd;

namespace Yi.Framework.Uow
{
    [DependsOn(
       typeof(YiFrameworkDddModule))]
    public class YiFrameworkUowModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.TryAddSingleton<IUnitOfWorkManager, DefaultUnitOfWorkManager>();
        }
    }
}
