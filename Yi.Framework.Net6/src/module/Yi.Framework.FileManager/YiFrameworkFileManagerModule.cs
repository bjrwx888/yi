using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;

namespace Yi.Framework.FileManager
{
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