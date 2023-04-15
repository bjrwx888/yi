using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Yi.Framework.Infrastructure.Sqlsugar.Repositories;
using Yi.Furion.Core.Rbac.ConstClasses;
using Yi.Furion.Core.Rbac.Dtos;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Sqlsugar.Core.Repositories.Impl
{
    public class UserRepository : SqlsugarRepository<UserEntity>, IUserRepository, ITransient
    {
        public UserRepository(ISqlSugarClient context) : base(context)
        {
        }



        /// <summary>
        /// 获取用户id的全部信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<UserRoleMenuDto> GetUserAllInfoAsync(long userId)
        {
            var userRoleMenu = new UserRoleMenuDto();
            //首先获取到该用户全部信息，导航到角色、菜单，(菜单需要去重,完全交给Set来处理即可)

            //得到用户
            var user = await _DbQueryable.Includes(u => u.Roles.Where(r => r.IsDeleted == false).ToList(), r => r.Menus.Where(m => m.IsDeleted == false).ToList()).InSingleAsync(userId);
            if (user is null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            user.Password = string.Empty;
            user.Salt = string.Empty;

            //超级管理员特殊处理
            if (UserConst.Admin.Equals(user.UserName))
            {
                userRoleMenu.User = user;
                userRoleMenu.RoleCodes.Add(UserConst.AdminRolesCode);
                userRoleMenu.PermissionCodes.Add(UserConst.AdminPermissionCode);
                return userRoleMenu;
            }

            //得到角色集合
            var roleList = user.Roles;

            //得到菜单集合
            foreach (var role in roleList)
            {
                userRoleMenu.RoleCodes.Add(role.RoleCode);

                if (role.Menus is not null)
                {
                    foreach (var menu in role.Menus)
                    {
                        if (!string.IsNullOrEmpty(menu.PermissionCode))
                        {
                            userRoleMenu.PermissionCodes.Add(menu.PermissionCode);
                        }
                        userRoleMenu.Menus.Add(menu);
                    }
                }

                //刚好可以去除一下多余的导航属性
                role.Menus = new List<MenuEntity>();
                userRoleMenu.Roles.Add(role);
            }

            user.Roles = new List<RoleEntity>();
            userRoleMenu.User = user;

            return userRoleMenu;
        }
    }
}
