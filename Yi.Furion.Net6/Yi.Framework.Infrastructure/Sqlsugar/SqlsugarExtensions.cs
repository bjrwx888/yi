using Microsoft.Extensions.DependencyInjection;

namespace Yi.Framework.Infrastructure.Sqlsugar
{
    /// <summary>
    /// 这一块，需要做成上下文对象，会进行重构
    /// </summary>
    public static class SqlsugarExtensions
    {
        //使用上下文对象
        public static void AddDbSqlsugarContextServer(this IServiceCollection services)
        {
            services.AddSingleton(x => x.GetRequiredService<SqlSugarDbContext>().SqlSugarClient);
            services.AddSingleton<SqlSugarDbContext>();
        }


    }
}
