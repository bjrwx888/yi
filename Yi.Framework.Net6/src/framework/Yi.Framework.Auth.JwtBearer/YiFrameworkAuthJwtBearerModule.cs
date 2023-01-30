using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Auth.JwtBearer.Authentication;
using Yi.Framework.Auth.JwtBearer.Authentication.Options;
using Yi.Framework.Auth.JwtBearer.Authorization;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.Configuration;

namespace Yi.Framework.Auth.JwtBearer
{
    [DependsOn(typeof(YiFrameworkCoreModule))]
    public class YiFrameworkAuthJwtBearerModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.Configure<JwtTokenOptions>(Appsettings.appConfiguration("JwtTokenOptions"));
            services.AddAuthentication(YiJwtAuthenticationHandler.YiJwtSchemeName);
            services.AddTransient<JwtTokenManager>();
            services.AddSingleton<IPermissionHandler, DefaultPermissionHandler>();
            services.AddAuthentication(option =>
            {
                option.AddScheme<YiJwtAuthenticationHandler>(YiJwtAuthenticationHandler.YiJwtSchemeName, YiJwtAuthenticationHandler.YiJwtSchemeName);
            });
            //services.AddSingleton<PermissionAttribute>();
            //services.AddControllers(options => {
            //    options.Filters.Add<PermissionAttribute>();
            //});
        }
    }
}
