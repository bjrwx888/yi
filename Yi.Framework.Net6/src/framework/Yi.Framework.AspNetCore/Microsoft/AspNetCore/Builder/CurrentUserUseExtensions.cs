using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Const;
using Yi.Framework.Core.CurrentUsers;

namespace Microsoft.AspNetCore.Builder
{
    public static class CurrentUserUseExtensions
    {
        public static IApplicationBuilder UseCurrentUserServer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CurrentUserMiddleware>();
        }


    }

    public class CurrentUserMiddleware
    {

        private readonly RequestDelegate _next;
        private ILogger<CurrentUserMiddleware> _logger;
        public CurrentUserMiddleware(RequestDelegate next, ILogger<CurrentUserMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, ICurrentUser _currentUser)
        {
            var authenticateContext = await context.AuthenticateAsync();
            if (authenticateContext.Principal is null)
            {
                _currentUser.IsAuthenticated = false;
                await _next(context);
                return;
            }
            var claims = authenticateContext.Principal.Claims;
            //通过鉴权之后，开始赋值
            _currentUser.IsAuthenticated = true;
            _currentUser.Id = claims.GetClaim(TokenTypeConst.Id) is null ? 0 : Convert.ToInt64(claims.GetClaim(TokenTypeConst.Id));
            _currentUser.UserName = claims.GetClaim(TokenTypeConst.UserName) ?? "";
            _currentUser.Permission = claims.GetClaims(TokenTypeConst.Permission);
            _currentUser.TenantId = claims.GetClaim(TokenTypeConst.TenantId) is null ? null : Guid.Parse(claims.GetClaim(TokenTypeConst.TenantId)!);
            await _next(context);

        }



    }

    public static class ClaimExtension
    {
        public static string? GetClaim(this IEnumerable<Claim> claims, string type)
        {
            return claims.Where(c => c.Type == type).Select(c => c.Value).FirstOrDefault();
        }

        public static string[]? GetClaims(this IEnumerable<Claim> claims, string type)
        {
            return claims.Where(c => c.Type == type).Select(c => c.Value).ToArray();
        }
    }
}
