using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Const;
using Yi.Framework.Infrastructure.CurrentUsers.Accessor;

namespace Yi.Framework.Infrastructure.CurrentUsers
{
    public class CurrentUser : ICurrentUser
    {
        private readonly ICurrentPrincipalAccessor _principalAccessor;
        public CurrentUser(ICurrentPrincipalAccessor principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }
        public bool IsAuthenticated => Id != 0;

        public long Id => FindUserId();

        public string UserName => this.FindClaimValue(TokenTypeConst.UserName);

        /// <summary>
        /// 暂时为默认值
        /// </summary>
        public Guid TenantId { get; set; } = Guid.Empty;

        public string Email => FindClaimValue(TokenTypeConst.Email);

        public bool EmailVerified => false;

        public string PhoneNumber => FindClaimValue(TokenTypeConst.PhoneNumber);

        public bool PhoneNumberVerified => false;

        public string[]? Roles => this.FindClaims(TokenTypeConst.Roles).Select(c => c.Value).Distinct().ToArray();

        public string[]? Permission => this.FindClaims(TokenTypeConst.Permission).Select(c => c.Value).Distinct().ToArray();

        public virtual Claim FindClaim(string claimType)
        {
            return _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == claimType);
        }

        public virtual Claim[] FindClaims(string claimType)
        {
            return _principalAccessor.Principal?.Claims.Where(c => c.Type == claimType).ToArray() ?? new Claim[0];
        }

        public virtual Claim[] GetAllClaims()
        {
            return _principalAccessor.Principal?.Claims.ToArray() ?? new Claim[0];
        }

        public string FindClaimValue(string claimType)
        {
            return FindClaim(claimType)?.Value;
        }


        public long FindUserId()
        {
            var userIdOrNull = _principalAccessor.Principal?.Claims?.FirstOrDefault(c => c.Type == TokenTypeConst.Id);
            if (userIdOrNull == null || string.IsNullOrWhiteSpace(userIdOrNull.Value))
            {
                return 0;
            }

            if (long.TryParse(userIdOrNull.Value, out long userId))
            {
                return userId;
            }

            return 0;
        }
    }
}