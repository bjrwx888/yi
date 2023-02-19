using AspNetCore.Microsoft.AspNetCore.Builder;
using StartupModules;
using Yi.Framework.AspNetCore;
using Yi.Framework.Auth.JwtBearer;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.Autofac;
using Yi.Framework.Data.Json;
using Yi.Template.Application;
using Yi.Template.Sqlsugar;

namespace Yi.Template.Web
{
    [DependsOn(
               typeof(YiFrameworkAspNetCoreModule),
          typeof(YiFrameworkCoreAutofacModule),
        typeof(YiTemplateSqlsugarModule),
        typeof(YiTemplateApplicationModule)
        )]
    public class YiTemplateWebModule : IStartupModule
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
                opt.CreateConventional(typeof(YiTemplateApplicationModule).Assembly, option => option.RootPath = string.Empty);
            });

            //添加swagger
            services.AddSwaggerServer<YiTemplateApplicationModule>();
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
