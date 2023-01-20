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
            var dataFilter = app.ApplicationServices.GetRequiredService<IDataFilter>();
            //内置多租户与软删除过滤
            dataFilter.AddFilter<ISoftDelete>(u => u.IsDeleted == false);

            //租户id从租户管理中获取
            dataFilter.AddFilter<IMultiTenant>(u => u.TenantId == Guid.Empty);
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //添加默认没有真正实现的
            services.AddTransient<IDataFilter, DefaultDataFilter>();
        }
    }
}
