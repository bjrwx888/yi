using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.CurrentUsers;

namespace Yi.Framework.Infrastructure.Auth
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
                if (_currentUser.Permission.Contains("*:*:*"))
                {
                    return true;
                }

                return _currentUser.Permission.Contains(permission);

            }

            return false;
        }
    }
}