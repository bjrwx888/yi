using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class UserService
    {
        public UserService(ISqlSugarClient context) : base(context)
        {
        }
    }
}
