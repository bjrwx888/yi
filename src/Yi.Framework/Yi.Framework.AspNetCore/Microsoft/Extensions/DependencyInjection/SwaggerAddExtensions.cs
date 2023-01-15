using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerAddExtensions
    {
        public static IServiceCollection AddSwaggerServer<Program>(this IServiceCollection services, string title = "Yi意框架-API接口")
        {
            var apiInfo = new OpenApiInfo
            {
                Title = title,
                Version = "v1",
                Contact = new OpenApiContact { Name = "橙子", Email = "454313500@qq.com", Url = new Uri("https://ccnetcore.com") }
            };
            #region 注册Swagger服务
            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((docName, description) => true);

                c.SwaggerDoc("v1", apiInfo);


                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                if (basePath is not null)
                {
                    foreach (var item in Directory.GetFiles(basePath, "*.xml"))
                    {
                        c.IncludeXmlComments(item, true);
                    }
                }

                c.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
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
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    [scheme] = new string[0]
                });
            });
            #endregion

            return services;
        }
    }
}
