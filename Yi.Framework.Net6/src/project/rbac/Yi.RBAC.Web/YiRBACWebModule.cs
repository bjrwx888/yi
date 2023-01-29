using AspNetCore.Microsoft.AspNetCore.Builder;
using StartupModules;
using Yi.Framework.Auth.JwtBearer;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;
using Yi.RBAC.Application;
using Yi.RBAC.Sqlsugar;

namespace Yi.RBAC.Web
{
    [DependsOn(
        typeof(YiRBACSqlsugarModule),
        typeof(YiRBACApplicationModule)
        )]
    public class YiRBACWebModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //添加控制器与动态api
            services.AddControllers();
            services.AddAutoApiService(opt =>
            {
                //NETServiceTest所在程序集添加进动态api配置
                opt.CreateConventional(typeof(YiRBACApplicationModule).Assembly, option => option.RootPath = string.Empty);
            });

            //添加swagger
            services.AddSwaggerServer<YiRBACApplicationModule>();
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
        }
    }
}
