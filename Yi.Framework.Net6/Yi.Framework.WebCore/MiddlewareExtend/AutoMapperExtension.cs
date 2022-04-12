using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Yi.Framework.WebCore.Mapper;

namespace Yi.Framework.WebCore.MiddlewareExtend
{
    /// <summary>
    /// 通用autoMapper扩展
    /// </summary>
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }
    }
}
