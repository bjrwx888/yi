﻿using AspNetCore.Microsoft.AspNetCore.Builder;
using StartupModules;
using Yi.Framework.Application;

namespace Yi.Framework.Web
{
    /// <summary>
    /// 这里是最后执行的模块
    /// </summary>
    public class YiFrameworkWebModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //添加控制器与动态api
            services.AddControllers();
            services.AddAutoApiService(opt =>
            {
                //NETServiceTest所在程序集添加进动态api配置
                opt.CreateConventional(typeof(YiFrameworkApplicationModule).Assembly, option => option.RootPath = string.Empty);
            });

            //添加swagger
            services.AddSwaggerServer<YiFrameworkApplicationModule>();
        }
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerServer();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();
            TimeTest.Result();

        }
    }
}