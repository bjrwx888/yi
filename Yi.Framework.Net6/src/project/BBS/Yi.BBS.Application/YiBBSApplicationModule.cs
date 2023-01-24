using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Application.Contracts;
using Yi.Framework.Auth.JwtBearer;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Data;
using Yi.Framework.Ddd;
using Yi.BBS.Domain;

namespace Yi.BBS.Application
{
    [DependsOn(
        typeof(YiBBSApplicationContractsModule),
        typeof(YiBBSDomainModule),
        typeof(YiFrameworkAuthJwtBearerModule)
        )]
    public class YiBBSApplicationModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
        }
    }
}
