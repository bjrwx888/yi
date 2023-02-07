using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Configuration;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Core.Model;

namespace Yi.Framework.Core
{
    public class YiFrameworkCoreModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            //服务定位实例赋值
            ServiceLocatorModel.Instance = app.ApplicationServices;

            //全局错误，需要靠前，放在此处无效
            //app.UseErrorHandlingServer();

            app.UseCurrentUserServer();
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //添加全局配置类
            services.AddSingleton(new Appsettings(context.Configuration));
            //全盘扫描,自动依赖注入
            services.AddAutoIocServer();

            services.AddCurrentUserServer();

        }
    }
}
