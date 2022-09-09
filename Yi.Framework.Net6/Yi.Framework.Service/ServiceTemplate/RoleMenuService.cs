using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class RoleMenuService : BaseService<RoleMenuEntity>, IRoleMenuService
    {
        public RoleMenuService(IRepository<RoleMenuEntity> repository) : base(repository)
        {
        }
    }
}
