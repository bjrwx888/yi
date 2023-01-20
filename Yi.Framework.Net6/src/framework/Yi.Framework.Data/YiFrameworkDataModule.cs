using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Entities;
using Yi.Framework.Data.Filters;

namespace Yi.Framework.Data
{
    public class YiFrameworkDataModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            var dataFilter = app.ApplicationServices.GetRequiredService<IDataFilter>();
            //内置多租户与软删除过滤
            dataFilter.AddFilter<ISoftDelete>(u => u.IsDeleted == false);

            //租户id从租户管理中获取
            dataFilter.AddFilter<IMultiTenant>(u => u.TenantId == Guid.Empty);
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddTransient<IDataFilter, DefaultDataFilter>();
        }
    }
}
