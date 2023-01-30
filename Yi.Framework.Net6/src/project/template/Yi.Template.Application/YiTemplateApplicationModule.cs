using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Template.Application.Contracts;
using Yi.Framework.Auth.JwtBearer;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Data;
using Yi.Framework.Ddd;
using Yi.Template.Domain;

namespace Yi.Template.Application
{
    [DependsOn(
        typeof(YiTemplateApplicationContractsModule),
        typeof(YiTemplateDomainModule),
        typeof(YiFrameworkAuthJwtBearerModule)
        )]
    public class YiTemplateApplicationModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
        }
    }
}
