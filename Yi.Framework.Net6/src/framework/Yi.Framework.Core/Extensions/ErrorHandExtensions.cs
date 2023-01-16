using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Exceptions;

namespace Yi.Framework.Core.Extensions
{
    public class ErrorHandMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandMiddleware> _logger;
        //private readonly IErrorHandle _errorHandle;
        public ErrorHandMiddleware(RequestDelegate next, ILoggerFactory loggerFactory /*, IErrorHandle errorHandle*/)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<ErrorHandMiddleware>();
        }
        public async Task InvokeAsync(HttpContext context)
        {
            bool isNext = true;
            try
            {
                await _next(context);
            }
            catch (BusinessException businessEx)
            {
                var statusCode = 200;
                //业务错误，不记录
                await context.Response.WriteAsync($"你好世界:友好错误，已经被中间件拦截");
            }
            catch (Exception ex)
            {
                isNext = false;
                _logger.LogError(ex, $"系统错误:{ex.Message}");
                //await _errorHandle.Invoer(context, ex);
                var statusCode = context.Response.StatusCode;
                context.Response.StatusCode = 500;
                //系统错误，需要记录
                await context.Response.WriteAsync($"你好世界：系统错误，已经被中间件拦截");
            }
        }

    }

    public static class ErrorHandExtensions
    {
        public static IApplicationBuilder UseErrorHandlingServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandMiddleware>();
        }
    }
}
