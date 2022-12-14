using System;
using System.Collections.Generic;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Quartz;
using Yi.Framework.WebCore.AutoFacExtend;
using Yi.Framework.WebCore.MiddlewareExtend;
using Microsoft.AspNetCore.Builder;
using Yi.Framework.WebCore.DbExtend;
using Microsoft.AspNetCore.Hosting;

namespace Yi.Framework.XUnitTest
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder host)
        {
            host.ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json");
                    builder.AddJsonFile("appsettings.Development.json");
                });
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                #region
                //交由Module依赖注入
                #endregion
                containerBuilder.RegisterModule<CustomAutofacModule>();
                #region
                //添加属性注入模块
                #endregion
                //containerBuilder.RegisterModule<PropertiesAutowiredModule>();
                #region
                //使用AppService特性优雅的进行自动依赖注入,仓储与基类服务便是使用该种方式自动注入
                #endregion
                containerBuilder.AddAutoIocService("Yi.Framework.Repository", "Yi.Framework.Service");
            });

            host.ConfigureServices(services => { });
        }
        private IServiceCollection _iServiceCollection;
        public void ConfigureServices(IServiceCollection services, HostBuilderContext host)
        {
            services.AddIocService(host.Configuration);
            ConfigureServices2(services);
            _iServiceCollection = services;
        }

        public void ConfigureServices2(IServiceCollection services)
        {
            services.AddQuartzService();
            services.AddSqlsugarServer();
            _iServiceCollection = services;
        }

        public void Configure(IServiceProvider services)
        {
            var appBuild = WebApplication.CreateBuilder();
            appBuild.WebHost.ConfigureServices(sc =>
            {
                ConfigureServices2(sc);
            });

            var app2 = appBuild.Build();
            app2.UseDbSeedInitService();
        }
    }
}
