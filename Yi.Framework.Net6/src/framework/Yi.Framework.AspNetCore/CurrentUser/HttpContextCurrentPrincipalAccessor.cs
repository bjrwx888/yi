using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.CurrentUsers.Accessor;

namespace Yi.Framework.AspNetCore.CurrentUser
{
    public class HttpContextCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextCurrentPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override ClaimsPrincipal GetClaimsPrincipal()
        {
            return _httpContextAccessor.HttpContext?.User ?? base.GetClaimsPrincipal();
        }

    }
}
