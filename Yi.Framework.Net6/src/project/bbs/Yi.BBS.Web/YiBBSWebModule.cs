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
using Yi.Framework.OperLogManager;
using Yi.Framework.Core.Module;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace Yi.BBS.Web
{
    [DependsOn(
         typeof(YiBBSSqlsugarModule),
               typeof(YiFrameworkAspNetCoreModule),
        typeof(YiFrameworkCoreAutofacModule),
        typeof(YiBBSApplicationModule),
           typeof(YiBBSSqlsugarModule)
        )]
    public class YiBBSWebModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //添加控制器与动态api
            services.AddControllers().AddJsonOptions(opt => {
                opt.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
                opt.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }); 

            services.AddAutoApiService(opt =>
            {
                //NETServiceTest所在程序集添加进动态api配置
                opt.CreateConventional(ModuleAssembly.Assemblies, option => option.RootPath = string.Empty);
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
