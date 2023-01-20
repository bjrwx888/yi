using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Data.Entities;
using Yi.Framework.Data.Extensions;
using Yi.Framework.Data.Filters;
using Yi.Framework.Ddd;

namespace Yi.Framework.Data
{
    [DependsOn(
        typeof(YiFrameworkDddModule))]
    public class YiFrameworkDataModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            //使用了过滤器
            app.UseDataFiterServer();
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //添加默认没有真正实现的
            services.AddTransient<IDataFilter, DefaultDataFilter>();
        }
    }
}
