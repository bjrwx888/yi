using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
using Yi.Framework.Uow;

namespace Yi.Framework.Data
{
    [DependsOn(
        typeof(YiFrameworkDddModule),
        typeof(YiFrameworkUowModule)) ]
    public class YiFrameworkDataModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
           

            //使用了过滤器
            app.UseDataFiterServer();

            //添加种子数据
            app.UseDataSeedServer();
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //添加默认没有真正实现的
            services.TryAddTransient<IDataFilter, DefaultDataFilter>();
        }
    }
}
