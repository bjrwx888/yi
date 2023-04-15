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
        private ICurrentUser _currentUser { get; set; }

        public DefaultPermissionHandler(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
        public bool IsPass(string permission)
        {
            if (_currentUser.Permission is not null)
            {
               return _currentUser.Permission.Contains(permission);

            }

            return false;
        }
    }
}
