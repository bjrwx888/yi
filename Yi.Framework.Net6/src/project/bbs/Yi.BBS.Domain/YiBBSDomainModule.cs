using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Data;
using Yi.BBS.Domain.Shared;

namespace Yi.BBS.Domain
{
    [DependsOn(
        typeof(YiBBSDomainSharedModule)
        )]
    public class YiBBSDomainModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {

        }
    }
}
