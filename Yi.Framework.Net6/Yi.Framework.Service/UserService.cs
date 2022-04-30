using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yi.Framework.DTOModel;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class UserService
    {
        public async Task<List<UserEntity>> DbTest()
        {
            return await _repository._Db.Queryable<UserEntity>().ToListAsync();
        }
        public async Task<bool> Exist(long id, Action<UserEntity> userAction = null)
        {
            var user = await _repository.GetByIdAsync(id);
            userAction.Invoke(user);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Exist(string userName, Action<UserEntity> userAction = null)
        {
            var user = await _repository.GetFirstAsync(u => u.UserName == userName);
            if (userAction != null)
            {
                userAction.Invoke(user);
            }
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Login(string userName, string password, Action<UserEntity> userAction = null)
        {
            var user = new UserEntity();
            if (await Exist(userName, o => user = o))
            {
                userAction.Invoke(user);
                if (user.Password == Common.Helper.MD5Helper.SHA2Encode(password, user.Salt))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> Register(UserEntity userEntity, Action<UserEntity> userAction = null)
        {
            var user = new UserEntity();
            if (!await Exist(userEntity.UserName))
            {
                user.UserName = userEntity.UserName;
                user.BuildPassword();
                userAction.Invoke(await _repository.InsertReturnEntityAsync(user));
                return true;
            }
            return false;
        }

        public async Task<List<UserEntity>> GetListInRole()
        {
            return await _repository._Db.Queryable<UserEntity>().Includes(u => u.Roles).ToListAsync();
        }

        public async Task<bool> GiveUserSetRole(List<long> userIds, List<long> roleIds)
        {
            var _repositoryUserRole = _repository.ChangeRepository<Repository<UserRoleEntity>>();

            //多次操作，需要事务确保原子性
            return await _repositoryUserRole.UseTranAsync(async () =>
             {

                 //遍历用户
                 foreach (var userId in userIds)
                 {
                     //删除用户之前所有的用户角色关系（物理删除，没有恢复的必要）
                     await _repositoryUserRole.DeleteAsync(u => u.UserId == userId);

                     //添加新的关系
                     List<UserRoleEntity> userRoleEntities = new();
                     foreach (var roleId in roleIds)
                     {
                         userRoleEntities.Add(new UserRoleEntity() { UserId = userId, RoleId = roleId });
                     }

                     //一次性批量添加
                     await _repositoryUserRole.InsertReturnSnowflakeIdAsync(userRoleEntities);
                 }
             });
        }


        public async Task<List<RoleEntity>> GetRoleListByUserId(long userId)
        {
            return (await _repository._Db.Queryable<UserEntity>().Includes(u => u.Roles).InSingleAsync(userId)).Roles;
        }

        public async Task<UserRoleMenuDto> GetUserAllInfo(long userId)
        {

            var userRoleMenu = new UserRoleMenuDto();
            //首先获取到该用户全部信息，导航到角色、菜单，(菜单需要去重,完全交给Set来处理即可)

            //得到用户
            var user = await _repository._Db.Queryable<UserEntity>().Includes(u => u.Roles, r => r.Menus).InSingleAsync(userId);

            //得到角色集合
            var roleList = user.Roles;

            //得到菜单集合
            foreach (var role in roleList)
            {
                foreach (var menu in role.Menus)
                {
                    userRoleMenu.Menus.Add(menu);
                }
                //刚好可以去除一下多余的导航属性
                role.Menus = null;
                userRoleMenu.Roles.Add(role);
            }

            user.Roles = null;
            userRoleMenu.User = user;

            return userRoleMenu;


        }
    }
}
