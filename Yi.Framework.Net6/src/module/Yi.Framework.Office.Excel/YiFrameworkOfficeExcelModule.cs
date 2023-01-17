using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;

namespace Yi.Framework.Office.Excel
{
    [DependsOn(
    typeof(YiFrameworkCoreModule)
    )]
    public class YiFrameworkOfficeExcelModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddExcelToObjectNpoiService();
        }
    }
}
