using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Data.Filters;

namespace Yi.Framework.Infrastructure.Data
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
            _next = next;
            _logger = loggerFactory.CreateLogger<DataFilterMiddleware>();
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