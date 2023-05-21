using Furion;
using Furion.Schedule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Profiling.SqlFormatters;
using Yi.Framework.Infrastructure.AspNetCore;
using Yi.Framework.Infrastructure.Auth;
using Yi.Framework.Infrastructure.Data;
using Yi.Framework.Infrastructure.Data.DataSeeds;
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
        services.AddTransient<IDataFilter, SqlsugarDataFilter>();


        services.AddSingleton<IPermissionHandler, DefaultPermissionHandler>();
        services.AddSingleton<PermissionGlobalAttribute>();
        services.AddControllers(options =>
        {
            options.Filters.Add<PermissionGlobalAttribute>();
        });


    }

    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSqlsugarCodeFirstServer();
        await app.UseDataSeedServer();
        app.UseDataFiterServer();

    }
}
