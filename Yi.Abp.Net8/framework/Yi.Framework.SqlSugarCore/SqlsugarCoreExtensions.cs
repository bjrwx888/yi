using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.SqlSugarCore
{
    public static class SqlsugarCoreExtensions
    {
        public static IServiceCollection AddYiDbContext<TDbContext>(this IServiceCollection service, ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TDbContext : class, ISqlSugarDbContextDependencies
        {
            service.Add(new ServiceDescriptor(typeof(ISqlSugarDbContextDependencies), typeof(TDbContext), serviceLifetime));
            return service;
        }
        
        public static IServiceCollection AddYiDbContext<TDbContext>(this IServiceCollection service, Action<DbConnOptions> options) where TDbContext : class, ISqlSugarDbContextDependencies
        {
            service.Configure<DbConnOptions>(ops =>
            {
                options.Invoke(ops);
            });
            service.AddYiDbContext<TDbContext>();
            return service;
        }
    }
}
