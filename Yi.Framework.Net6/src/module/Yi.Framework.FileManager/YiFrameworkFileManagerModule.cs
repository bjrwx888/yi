using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core;
using Yi.Framework.Ddd;

namespace Yi.Framework.FileManager
{
    [DependsOn(
          typeof(YiFrameworkDddModule)
        )]
    public class YiFrameworkFileManagerModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
       
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddTransient<FileService>();
            services.AddTransient<IFileService,FileService>();
        }
    }
}