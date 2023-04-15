using System.Security.Claims;

namespace Yi.Framework.Infrastructure.CurrentUsers.Accessor
{
    public interface ICurrentPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
        IDisposable Change(ClaimsPrincipal principal);
    }

}
