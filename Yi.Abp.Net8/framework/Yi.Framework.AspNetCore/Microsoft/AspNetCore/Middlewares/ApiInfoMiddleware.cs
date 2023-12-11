using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace Yi.Framework.AspNetCore.Microsoft.AspNetCore.Middlewares
{
    public class ApiInfoMiddleware : IMiddleware, ITransientDependency
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);
        }
    }
}
