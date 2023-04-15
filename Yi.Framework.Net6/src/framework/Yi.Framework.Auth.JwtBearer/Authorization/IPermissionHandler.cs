using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.CurrentUsers;

namespace Yi.Framework.Auth.JwtBearer.Authorization
{
    public interface IPermissionHandler
    {
        bool IsPass(string permission );
    }
}
