using System.Security.Claims;

namespace Yi.Framework.Core.CurrentUsers.Accessor
{
    public interface ICurrentPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
        IDisposable Change(ClaimsPrincipal principal);
    }

}
