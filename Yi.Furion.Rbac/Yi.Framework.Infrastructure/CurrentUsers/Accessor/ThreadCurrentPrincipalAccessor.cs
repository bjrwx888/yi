using System.Security.Claims;

namespace Yi.Framework.Infrastructure.CurrentUsers.Accessor
{
    public class ThreadCurrentPrincipalAccessor : CurrentPrincipalAccessorBase
    {
        protected override ClaimsPrincipal GetClaimsPrincipal()
        {
            return Thread.CurrentPrincipal as ClaimsPrincipal;
        }
    }

}
