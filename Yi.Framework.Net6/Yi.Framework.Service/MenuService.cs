using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class MenuService : BaseService<MenuEntity>, IMenuService
    {
        public async Task<List<MenuEntity>> SelctGetList(MenuEntity menu)
        {
            var data = await _repository._Db.Queryable<MenuEntity>()
                    .WhereIF(!string.IsNullOrEmpty(menu.MenuName), u => u.MenuName.Contains(menu.MenuName))
                     .Where(u => u.IsDeleted == false)
                    .OrderBy(u => u.OrderNum, OrderByType.Desc)
                    .ToListAsync();
            return data;
        }
        public async Task<List<MenuEntity>> GetMenuTreeAsync()
        {
            //ParentId 0,代表为根目录，只能存在一个
            //复杂查询直接使用db代理
            return await _repository._Db.Queryable<MenuEntity>().Where(u => u.IsDeleted == false).ToTreeAsync(it => it.Children, it => it.ParentId, 0);
        }
    }
}
