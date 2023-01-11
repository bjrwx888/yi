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

                //添加注释服务
                //为 Swagger JSON and UI设置xml文档注释路径
                //获取应用程序所在目录(绝对路径，不受工作目录影响，建议采用此方法获取路径使用windwos&Linux）
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                if (basePath is null)
                {
                    throw new Exception("未找到swagger文件");
                }
                var apiXmlPath = Path.Combine(basePath, @"SwaggerDoc.xml");//控制器层注释
                                                                           //var entityXmlPath = Path.Combine(basePath, @"SwaggerDoc.xml");//实体注释
                                                                           //c.IncludeXmlComments(apiXmlPath, true);//true表示显示控制器注释
                                                                           //c.IncludeXmlComments(apiXmlPath, true);

                //这里路径应该动态获取，先暂时写死
                //c.IncludeXmlComments("E:\\Yi\\src\\Yi.Framework\\Yi.Framework.Application\\SwaggerDoc.xml", true);


                //添加控制器注释
                //c.DocumentFilter<SwaggerDocTag>();

                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                //var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
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
