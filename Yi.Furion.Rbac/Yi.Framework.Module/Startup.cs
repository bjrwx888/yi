using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yi.Framework.Infrastructure.AspNetCore;
using Yi.Framework.Infrastructure.Sqlsugar;

namespace Yi.Framework.Module;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCurrentUserServer();

        services.Configure<DbConnOptions>(App.Configuration.GetSection("DbConnOptions"));

        services.AddDbSqlsugarContextServer();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

    }
}
