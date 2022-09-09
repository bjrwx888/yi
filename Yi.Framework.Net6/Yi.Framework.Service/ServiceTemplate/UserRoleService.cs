using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class UserRoleService : BaseService<UserRoleEntity>, IUserRoleService
    {
        public UserRoleService(IRepository<UserRoleEntity> repository) : base(repository)
        {
        }
    }
}
