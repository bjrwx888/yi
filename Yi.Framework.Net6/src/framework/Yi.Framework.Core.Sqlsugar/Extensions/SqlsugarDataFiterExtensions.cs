using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Sqlsugar.Extensions
{
    public static class SqlsugarDataFilterExtensions
    {
        public static IApplicationBuilder UseSqlsugarDataFiterServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SqlsugarDataFilterMiddleware>();
        }
    }

    public class SqlsugarDataFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SqlsugarDataFilterMiddleware> _logger;
        public SqlsugarDataFilterMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<SqlsugarDataFilterMiddleware>();
        }
        public async Task InvokeAsync(HttpContext context)
        {

            await _next(context);

        }
    }

}