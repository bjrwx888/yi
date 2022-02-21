using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Enum;
using Yi.Framework.Common.Helper;
using Yi.Framework.Core;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Service
{
   public partial class MenuService:BaseService<menu>, IMenuService
    {
        short Normal = (short)DelFlagEnum.Normal;
        public async Task<menu> AddChildrenMenu(int menu_id, menu _children)
        {
            _children.parentId = menu_id;
            _children.is_top = (short)TopFlagEnum.Children;
            _children.is_delete = (short)DelFlagEnum.Normal;
            await AddAsync(_children);
            return _children;
        }

        public async Task<bool> AddTopMenu(menu _menu)
        {
            _menu.is_top = (short)TopFlagEnum.Children;

            return await AddAsync(_menu);
        }

        public async Task<menu> GetMenuInMould()
        {
            var menu_data = await _DbRead.Set<menu>().Include(u => u.mould).Where(u=>u.is_delete==(short)DelFlagEnum.Normal).ToListAsync();
            return TreeHelper.SetTree(menu_data, null)[0]; ; 
        }


        public async Task<List<menu>> GetTopMenusByTopMenuIds(List<int> menuIds)
        {
           return await _DbRead.Set<menu>().AsNoTracking().Where(u => menuIds.Contains(u.id)).OrderBy(u=>u.sort).ToListAsync();
        }

        public async Task<menu> SetMouldByMenu(int id1,int id2)
        {
            var menu_data = await _DbRead.Set<menu>().Include(u => u.mould).Where(u => u.id == id1).FirstOrDefaultAsync();
            var mould_data = await _DbRead.Set<mould>().Where(u => u.id == id1).FirstOrDefaultAsync();
            menu_data.mould = mould_data;
            _Db.Update(menu_data);
            return menu_data;
        }

 
        public async Task<List<menu>> GetTopMenuByUserId(int userId)
        {
            var user_data = await _DbRead.Set<user>().Include(u => u.roles).ThenInclude(u => u.menus).Where(u=>u.id==userId).FirstOrDefaultAsync();
            List<menu> menuList = new();
            user_data.roles.ForEach(u =>
            {
                var m = u.menus.Where(u => u.is_delete == Normal).ToList();
                menuList = menuList.Union(m).ToList();
            });

            var menuIds=menuList.Select(u => u.id).ToList();

            return await _DbRead.Set<menu>().Include(u => u.mould).Where(u => menuIds.Contains(u.id)).ToListAsync();
        }

    }
}
