using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;
using Yi.RBAC.Domain.Shared;

namespace Yi.RBAC.Application.Contracts
{
    [DependsOn(
        typeof(YiRBACDomainSharedModule)
        )]
    public class YiRBACApplicationContractsModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
        }
    }
}
