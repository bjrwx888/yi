using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class RoleService : BaseService<RoleEntity>, IRoleService
    {
        public RoleService(IRepository<RoleEntity> repository) : base(repository)
        {
        }
    }
}
