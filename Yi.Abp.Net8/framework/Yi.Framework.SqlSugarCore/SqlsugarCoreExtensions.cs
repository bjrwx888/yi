﻿using System;
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
        public static IServiceCollection AddYiDbContext<DbContext>(this IServiceCollection service) where DbContext : class, ISqlSugarDbContext
        {
            service.Replace(new ServiceDescriptor(typeof(ISqlSugarDbContext), typeof(DbContext), ServiceLifetime.Scoped));
            return service;
        }

        public static IServiceCollection AddYiDbContext<DbContext>(this IServiceCollection service,Action<DbConnOptions> options) where DbContext : class, ISqlSugarDbContext
        {
        
            service.Configure<DbConnOptions>(ops => {
                options.Invoke(ops);
            });
            service.AddYiDbContext<DbContext>();
            return service;
        }
    }
}
