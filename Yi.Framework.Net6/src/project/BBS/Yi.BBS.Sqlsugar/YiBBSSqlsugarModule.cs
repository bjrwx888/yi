using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.Sqlsugar;
using Yi.BBS.Domain;

namespace Yi.BBS.Sqlsugar
{
    [DependsOn(typeof(YiFrameworkCoreSqlsugarModule),
        typeof(YiBBSDomainModule))]
    public class YiBBSSqlsugarModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //services.AddTransient<IStudentRepository, StudentRepository>();
        }
    }
}
