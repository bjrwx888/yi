using System.Security.Claims;

using Yi.Framework.Core.CurrentUsers.Accessor;

namespace SF.CurrentUser.CS.Accessor
{
    public class StaticPrincipalAccessor : CurrentPrincipalAccessorBase
    {
        public static ClaimsPrincipal ClaimsPrincipal { get; set; }
        protected override ClaimsPrincipal GetClaimsPrincipal()
        {
            return ClaimsPrincipal;
        }
    }
}
