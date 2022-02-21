using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Const;
using Yi.Framework.Common.Helper;
using Yi.Framework.Core;
using Yi.Framework.DTOModel;
using Yi.Framework.Interface;
using Yi.Framework.Model;
using Yi.Framework.Model.ModelFactory;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Service
{
    public partial class UserService : BaseService<user>, IUserService
    {
        CacheClientDB _cacheClientDB;
        public UserService(CacheClientDB cacheClientDB, IDbContextFactory DbFactory) : base(DbFactory)
        {
            _cacheClientDB = cacheClientDB;
        }
        short Normal = (short)Common.Enum.DelFlagEnum.Normal;
        public async Task<bool> PhoneIsExsit(string smsAddress)
        {
            var userList = await GetEntity(u => u.phone == smsAddress);
            if (userList == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> EmailIsExsit(string emailAddress)
        {
            var userList = await GetEntity(u => u.email == emailAddress);
            if (userList == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<user> GetUserById(int userId)
        {
            var user_data = await _DbRead.Set<user>().Include(u => u.roles).ThenInclude(u => u.menus).ThenInclude(u => u.mould).Where(u => u.id == userId).FirstOrDefaultAsync();
            return user_data;

        }
        public async Task<List<menu>> GetAxiosByRouter(string router, List<int> menuIds)
        {
           var menu_data= await _DbRead.Set<menu>().Where(u => u.router.Trim().ToUpper() == router.Trim().ToUpper() && u.is_delete == (short)Common.Enum.DelFlagEnum.Normal).FirstOrDefaultAsync();
            return await _DbRead.Set<menu>().Include(u=>u.mould).Where(u => u.parentId == menu_data.id && u.is_delete == (short)Common.Enum.DelFlagEnum.Normal).ToListAsync();
        }

        public async Task<menu> GetMenuByHttpUser(List<int> allMenuIds)
        {
            var topMenu = await _DbRead.Set<menu>().Where(u => allMenuIds.Contains(u.id)&& u.is_show == (short)Common.Enum.ShowFlagEnum.Show && u.is_delete == (short)Common.Enum.DelFlagEnum.Normal).ToListAsync();
            //现在要开始关联菜单了
            return TreeHelper.SetTree(topMenu)[0];
        }
        public async Task<user> GetUserInRolesByHttpUser(int userId)
        {
            var data = await GetUserById(userId);
            data.roles?.ForEach(u =>
            {
                u.users = null;
                u.menus = null;
            });
            return data;
        }

        public async Task<user> Login(user _user)
        {
            var user_data = await _DbRead.Set<user>().Include(u => u.roles).Where(u => u.username == _user.username && u.password == _user.password && u.is_delete == Normal).FirstOrDefaultAsync();
            return user_data;
        }

        public async Task<bool> Register(user _user)
        {
            var user_data = await GetEntity(u => u.username == _user.username);
            if (user_data != null)
            {
                return false;
            }
            return await UpdateAsync(_user);
        }

        public async Task<bool> SetRoleByUser(List<int> roleIds, List<int> userIds)
        {
            var user_data = await _DbRead.Set<user>().Include(u => u.roles).Where(u => userIds.Contains(u.id)).ToListAsync();
            var roleList = await _DbRead.Set<role>().Where(u => roleIds.Contains(u.id)).ToListAsync();
            user_data.ForEach(u => u.roles = roleList);
            return await UpdateListAsync(user_data);
        }

        public bool SaveUserApi(int userId, List<menuDto> menus)
        {
            return _cacheClientDB.Set(RedisConst.userMenusApi + ":" + userId.ToString(), menus, new TimeSpan(0, 30, 0));
        }
        public List<int> GetCurrentMenuInfo(int userId)
        {
            return _cacheClientDB.Get<List<menuDto>>(RedisConst.userMenusApi + ":" + userId).Select(u => u.id).ToList();
        }
    }
}
