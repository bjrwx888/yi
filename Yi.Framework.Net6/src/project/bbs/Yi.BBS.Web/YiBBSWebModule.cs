using AspNetCore.Microsoft.AspNetCore.Builder;
using StartupModules;
using Yi.Framework.Auth.JwtBearer;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;
using Yi.BBS.Application;
using Yi.BBS.Sqlsugar;
using Yi.Framework.AspNetCore.Microsoft.Extensions.DependencyInjection;
using Yi.Framework.Core.Autofac;
using Yi.RBAC.Application;
using Yi.Framework.AspNetCore;
using Yi.Framework.Data.Json;

namespace Yi.BBS.Web
{
    [DependsOn(
               typeof(YiFrameworkAspNetCoreModule),
        typeof(YiFrameworkCoreAutofacModule),
        typeof(YiBBSSqlsugarModule),
        typeof(YiBBSApplicationModule)
        )]
    public class YiBBSWebModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //添加控制器与动态api
            services.AddControllers().AddJsonOptions(opt => {
                opt.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            }); 
            services.AddAutoApiService(opt =>
            {
                //NETServiceTest所在程序集添加进动态api配置
                opt.CreateConventional(typeof(YiBBSApplicationModule).Assembly, option => option.RootPath = string.Empty);
                //opt.CreateConventional(typeof(YiRBACApplicationModule).Assembly, option => option.RootPath = string.Empty);
            });

            //添加swagger
            services.AddSwaggerServer<YiBBSApplicationModule>();
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
