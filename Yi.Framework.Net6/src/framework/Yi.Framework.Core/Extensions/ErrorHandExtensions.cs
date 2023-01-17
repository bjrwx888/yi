using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Exceptions;

namespace Yi.Framework.Core.Extensions
{

   internal class ExceptionModle
    {
        public string? Message { get; set; }
        public string? Details { get; set; }
    }
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
            try
            {
                await _next(context);
            }
            catch (BusinessException businessEx)
            {
                context.Response.ContentType = "application/json;charset=utf-8";
                context.Response.StatusCode = businessEx.Code.GetHashCode();

                var result = new ExceptionModle
                {
                    Message= businessEx.Message,
                    Details= businessEx.Details,
                };
                //业务错误，不记录日志
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result, new JsonSerializerSettings()
                {
                    //设置首字母小写
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                }));

            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json;charset=utf-8";
                //系统错误，记录日志
                _logger.LogError(ex, $"系统错误:{ex.Message}");
                //await _errorHandle.Invoer(context, ex);
                context.Response.StatusCode = 500;
                //系统错误，需要记录
                var result = new ExceptionModle
                {
                    Message = ex.Message,
                    Details = "系统错误",
                };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result, new JsonSerializerSettings()
                {
                    //设置首字母小写
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                }));
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
