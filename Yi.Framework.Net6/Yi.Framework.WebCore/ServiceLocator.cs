using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using Ubiety.Dns.Core.Common;

namespace Yi.Framework.WebCore
{
    public static class ServiceLocator
    {
        public static IServiceProvider? Instance { get; set; }

        public static string Admin { get; set; } = "cc";

        public static bool GetHttp(out HttpContext? httpContext)
        {
            httpContext = null;
            var httpContextAccessor = Instance?.GetService<IHttpContextAccessor>();
            if (httpContextAccessor is null)
            {
                return false;
            }
            httpContext = httpContextAccessor.HttpContext;
            return true;
        }
    }

}
