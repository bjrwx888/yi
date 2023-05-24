using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Schema;
using Yi.Framework.Infrastructure.Data;
using Yi.Framework.Infrastructure.Data.DataSeeds;
using Yi.Framework.Infrastructure.Sqlsugar;

namespace Yi.Furion.Sqlsugar.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbSqlsugarContextServer<YiDbContext>();
    }
    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDataFiterServer();
        app.UseSqlsugarCodeFirstServer();
        await app.UseDataSeedServer();
    }

}
