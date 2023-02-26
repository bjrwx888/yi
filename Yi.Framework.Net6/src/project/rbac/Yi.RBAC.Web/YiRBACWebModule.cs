using AspNetCore.Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using StartupModules;
using Yi.Framework.AspNetCore;
using Yi.Framework.Auth.JwtBearer;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.Autofac;
using Yi.Framework.Data.Json;
using Yi.Framework.OperLogManager;
using Yi.RBAC.Application;
using Yi.RBAC.Sqlsugar;

namespace Yi.RBAC.Web
{
    [DependsOn(
        typeof(YiFrameworkAspNetCoreModule),
          typeof(YiFrameworkCoreAutofacModule),
        typeof(YiRBACSqlsugarModule),
        typeof(YiRBACApplicationModule)
        )]
    public class YiRBACWebModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            //添加控制器与动态api
            services.AddControllers().AddJsonOptions(opt => {

                opt.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));

            });


            
            services.AddAutoApiService(opt =>
            {
                opt.CreateConventional(AssemblyHelper.GetAllLoadAssembly(), option => option.RootPath = string.Empty);

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
