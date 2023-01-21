using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Application.Contracts;
using Yi.Framework.Application.Contracts.Student;
using Yi.Framework.Application.Student;
using Yi.Framework.Auth.JwtBearer;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Data;
using Yi.Framework.Ddd;
using Yi.Framework.Domain;

namespace Yi.Framework.Application
{
    [DependsOn(
        typeof(YiFrameworkApplicationContractsModule),
        typeof(YiFrameworkDomainModule),
        typeof(YiFrameworkAuthJwtBearerModule)
        )]
    public class YiFrameworkApplicationModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
        }
    }
}
