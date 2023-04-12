using System.Security.Claims;

namespace Yi.Framework.Infrastructure.CurrentUsers.Accessor
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
