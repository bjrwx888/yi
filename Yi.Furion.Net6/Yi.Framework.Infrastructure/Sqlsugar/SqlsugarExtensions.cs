using Furion;
using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.Infrastructure.Sqlsugar.Uow;

namespace Yi.Framework.Infrastructure.Sqlsugar
{
    /// <summary>
    /// 这一块，需要做成上下文对象，会进行重构
    /// </summary>
    public static class SqlsugarExtensions
    {
        /// <summary>
        /// 使用默认上下文
        /// </summary>
        /// <param name="services"></param>
        public static void AddDbSqlsugarContextServer(this IServiceCollection services)
        {
            services.AddDbSqlsugarOption();


            services.AddSingleton(x => x.GetRequiredService<SqlSugarDbContext>().SqlSugarClient);
            services.AddSingleton<SqlSugarDbContext>();


        }

        /// <summary>
        /// 自定义上下文
        /// </summary>
        /// <typeparam name="DbContext"></typeparam>
        /// <param name="services"></param>
        public static void AddDbSqlsugarContextServer<DbContext>(this IServiceCollection services) where DbContext : SqlSugarDbContext
        {
            services.AddDbSqlsugarOption();


            services.AddSingleton(x => x.GetRequiredService<DbContext>().SqlSugarClient);
            services.AddSingleton<DbContext>();

        }

        public static void AddDbSqlsugarOption(this IServiceCollection services)
        {
            services.Configure<DbConnOptions>(App.Configuration.GetSection("DbConnOptions"));

            services.AddUnitOfWork<SqlsugarUnitOfWork>();

        }
    }
}
