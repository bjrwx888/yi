using Furion;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Schema;
using Yi.Framework.Infrastructure.Sqlsugar;

namespace Yi.Furion.Sqlsugar.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbSqlsugarContextServer<YiDbContext>();
    }
}
