using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.CurrentUsers;

namespace Yi.Framework.Auth.JwtBearer.Authorization
{
    public class DefaultPermissionHandler : IPermissionHandler
    {
        public bool IsPass(string permission, ICurrentUser currentUser)
        {
            return true;
        }
    }
}
