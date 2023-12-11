using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Volo.Abp.AspNetCore.Mvc;

namespace Yi.Framework.AspNetCore.Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerAddExtensions
    {
        public static IServiceCollection AddYiSwaggerGen<Program>(this IServiceCollection services, Action<SwaggerGenOptions>? action=null)
        {
            var serviceProvider = services.BuildServiceProvider();
            var mvcOptions = serviceProvider.GetRequiredService<IOptions<AbpAspNetCoreMvcOptions>>();

            var mvcSettings = mvcOptions.Value.ConventionalControllers.ConventionalControllerSettings.DistinctBy(x => x.RemoteServiceName);


            services.AddAbpSwaggerGen(
            options =>
            {
                if (action is not null)
                {
                    action.Invoke(options);
                }

                // 配置分组,还需要去重,支持重写,如果外部传入后，将以外部为准
                foreach (var setting in mvcSettings.OrderBy(x => x.RemoteServiceName))
                {
                    if (!options.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey(setting.RemoteServiceName))
                    {
                        options.SwaggerDoc(setting.RemoteServiceName, new OpenApiInfo { Title = setting.RemoteServiceName, Version = "v1" });
                    }
                }

                // 根据分组名称过滤 API 文档
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (apiDesc.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        var settingOrNull = mvcSettings.Where(x => x.Assembly == controllerActionDescriptor.ControllerTypeInfo.Assembly).FirstOrDefault();
                        if (settingOrNull is not null)
                        {
                            return docName == settingOrNull.RemoteServiceName;
                        }
                    }
                    return false;
                });

                options.CustomSchemaIds(type => type.FullName);
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                if (basePath is not null)
                {
                    foreach (var item in Directory.GetFiles(basePath, "*.xml"))
                    {
                        options.IncludeXmlComments(item, true);
                    }
                }

                options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
                {
                    Description = "直接输入Token即可",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                var scheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "JwtBearer" }
                };
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    [scheme] = new string[0]
                });
            }
        );



            return services;
        }
    }
}
