using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Entities;
using Yi.Framework.Data.Filters;

namespace Yi.Framework.Data.Extensions
{
    public static class DataFilterExtensions
    {
        public static IApplicationBuilder UseDataFiterServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DataFilterMiddleware>();
        }
    }

    public class DataFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DataFilterMiddleware> _logger;
        public DataFilterMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<DataFilterMiddleware>();
        }
        public async Task InvokeAsync(HttpContext context, IDataFilter dataFilter)
        {
            //添加默认的过滤器
            dataFilter.AddFilter<ISoftDelete>(u => u.IsDeleted == false);
            //dataFilter.AddFilter<IMultiTenant>(u => u.TenantId == Guid.Empty);
            await _next(context);

        }
    }

}