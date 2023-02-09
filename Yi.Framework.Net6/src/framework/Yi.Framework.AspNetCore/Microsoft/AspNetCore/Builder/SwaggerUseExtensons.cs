using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Microsoft.AspNetCore.Builder
{
    public static class SwaggerUseExtensons
    {
        public static IApplicationBuilder UseSwaggerServer(this IApplicationBuilder app, params SwaggerModel[] swaggerModels)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (swaggerModels.Length == 0)
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yi.Framework");
                }
                else
                {
                    foreach (var k in swaggerModels)
                    {

                        c.SwaggerEndpoint(k.Url, k.Name);
                    }
                }

            });
            return app;
        }

    }
    public class SwaggerModel
    {
        public SwaggerModel(string name)
        {
            this.Name = name;
            this.Url = "/swagger/v1/swagger.json";
        }
        public SwaggerModel(string url, string name)
        {
            this.Url = url;
            this.Name = name;
        }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
