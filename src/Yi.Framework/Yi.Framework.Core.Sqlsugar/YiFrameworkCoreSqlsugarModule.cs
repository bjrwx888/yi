using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core.Configuration;
using Yi.Framework.Core.Sqlsugar.Extensions;
using Yi.Framework.Core.Sqlsugar.Options;
using Yi.Framework.Core.Sqlsugar.Repositories;
using Yi.Framework.Ddd;
using Yi.Framework.Ddd.Repositories;

namespace Yi.Framework.Core.Sqlsugar
{
    public class YiFrameworkCoreSqlsugarModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            app.UseSqlsugarCodeFirstServer();
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddTransient(typeof(IRepository<>), typeof(SqlsugarRepository<>));
            services.Configure<DbConnOptions>(Appsettings.appConfiguration("DbConnOptions"));
            services.AddSqlsugarServer();
       
        }
    }
}