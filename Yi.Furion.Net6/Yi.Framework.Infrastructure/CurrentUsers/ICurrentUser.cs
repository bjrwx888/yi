using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.CurrentUsers
{
    public interface ICurrentUser
    {
        public bool IsAuthenticated { get; }
        public long Id { get; }

        public long DeptId { get; }

        public string UserName { get; }

        public Guid TenantId { get; }

        public string Email { get; }

        public bool EmailVerified { get; }

        public string PhoneNumber { get; }

        public bool PhoneNumberVerified { get; }

        public string[]? Roles { get; }

        public string[]? Permission { get; }

    }
}
