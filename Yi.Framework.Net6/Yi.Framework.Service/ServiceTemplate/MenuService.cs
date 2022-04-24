using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class MenuService : BaseService<MenuEntity>, IMenuService
    {
        public MenuService(IRepository<MenuEntity> repository) : base(repository)
        {
        }
    }
}
