using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StartupModules;
using Yi.Framework.Core.Configuration;
using Yi.Framework.Core.Sqlsugar.Extensions;
using Yi.Framework.Core.Sqlsugar.Filters;
using Yi.Framework.Core.Sqlsugar.Options;
using Yi.Framework.Core.Sqlsugar.Repositories;
using Yi.Framework.Core.Sqlsugar.Uow;
using Yi.Framework.Data.Filters;
using Yi.Framework.Ddd;
using Yi.Framework.Ddd.Repositories;
using Yi.Framework.Uow;

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
            services.Replace(new ServiceDescriptor(typeof(IUnitOfWorkManager), typeof(SqlsugarUnitOfWorkManager), ServiceLifetime.Singleton));
            services.Replace(new ServiceDescriptor(typeof(IDataFilter), typeof(SqlsugarDataFilter), ServiceLifetime.Scoped));
            services.Configure<DbConnOptions>(Appsettings.appConfiguration("DbConnOptions"));
            services.AddSqlsugarServer();

        }
    }
}