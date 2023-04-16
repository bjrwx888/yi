using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Profiling.SqlFormatters;
using Yi.Framework.Infrastructure.AspNetCore;
using Yi.Framework.Infrastructure.Data;
using Yi.Framework.Infrastructure.Data.Filters;
using Yi.Framework.Infrastructure.Sqlsugar;
using Yi.Framework.Infrastructure.Sqlsugar.Filters;
using Yi.Framework.Infrastructure.Sqlsugar.Uow;

namespace Yi.Framework.Infrastructure;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCurrentUserServer();

        services.Configure<DbConnOptions>(App.Configuration.GetSection("DbConnOptions"));

        services.AddDbSqlsugarContextServer();

        services.AddUnitOfWork<SqlsugarUnitOfWork>();

        services.AddTransient<IDataFilter, SqlsugarDataFilter>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDataFiterServer();
    }
}
