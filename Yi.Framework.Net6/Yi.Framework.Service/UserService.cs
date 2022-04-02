using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Const;
using Yi.Framework.Common.Helper;
using Yi.Framework.Core;
using Yi.Framework.Interface;
using Yi.Framework.Model;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class UserService : Repository<User>, IUserService
    {
     
    }
}
