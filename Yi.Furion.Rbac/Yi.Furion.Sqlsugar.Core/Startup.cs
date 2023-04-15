using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace Yi.Furion.Sqlsugar.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddDatabaseAccessor(options =>
        //{
        //    options.AddDbPool<DefaultDbContext>();
        //}, "Yi.Furion.Rbac.Database.Migrations");
        System.Console.WriteLine();
    }
}
