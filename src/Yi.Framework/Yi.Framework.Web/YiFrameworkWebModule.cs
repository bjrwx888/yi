using Microsoft.AspNetCore.Cors;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Yi.Framework.Application;
using Yi.Framework.Sqlsugar;

namespace Yi.Framework.Web
{
    [DependsOn(
        typeof(AbpSwashbuckleModule),
        typeof(YiFrameworkApplicationModule),
        typeof(YiFrameworkSqlsugarModule),
        typeof(AbpAspNetCoreMvcModule)
        
        )]
    public class YiFrameworkWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            ConfigureAutoApiControllers();


            ConfigureSwaggerServices(context.Services);
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "YiFramework API");
            });
            app.UseConfiguredEndpoints();
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(YiFrameworkApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "YiFramework API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                    var basePath = Path.GetDirectoryName(this.GetType().Assembly.Location);
                    if (basePath is not null)
                    {
                        foreach (var item in Directory.GetFiles(basePath, "*.xml"))
                        {
                            options.IncludeXmlComments(item, true);
                        }
                    }
                
                }
            );
        }
    }
}
