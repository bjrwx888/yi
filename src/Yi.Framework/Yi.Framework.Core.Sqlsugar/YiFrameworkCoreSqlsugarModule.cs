﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core.Sqlsugar.Extensions;
using Yi.Framework.Core.Sqlsugar.Repository;
using Yi.Framework.Ddd;
using Yi.Framework.Ddd.Repository;

namespace Yi.Framework.Core.Sqlsugar
{
    public class YiFrameworkCoreSqlsugarModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddTransient(typeof(IRepository<>), typeof(SqlsugarRepository<>));
            services.AddSqlsugarServer();
        }
    }
}