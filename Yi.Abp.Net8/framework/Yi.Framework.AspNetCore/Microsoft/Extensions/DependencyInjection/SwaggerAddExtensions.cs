using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Yi.Framework.AspNetCore.Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerAddExtensions
    {
        public static IServiceCollection AddYiSwaggerGen<Program>(this IServiceCollection services, Action<SwaggerGenOptions>? action)
        {
            services.AddAbpSwaggerGen(
            options =>
            {
                options.DocInclusionPredicate((docName, description) => true);
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


                if (action is not null)
                {
                    action.Invoke(options);
                }
            }
        );



            return services;
        }
    }
}
