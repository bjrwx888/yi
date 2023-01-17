using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Autofac.Extensions;
using Yi.Framework.Core.Attributes;

namespace Yi.Framework.Core.AutoMapper
{
    [DependsOn(
    typeof(YiFrameworkCoreModule)
    )]
    public class YiFrameworkCoreAutoMapperModule : IStartupModule
    {

        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {

            //添加全局自动mapper
            services.AddAutoMapperService();
        }
    }
}