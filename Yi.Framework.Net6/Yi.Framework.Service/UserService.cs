using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        public async Task<bool> Exist(Guid id, Action<UserEntity> userAction = null)
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
                user.Salt = Common.Helper.MD5Helper.GenerateSalt();
                user.Password = Common.Helper.MD5Helper.SHA2Encode(userEntity.Password, user.Salt);
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
                    await _repositoryUserRole.InsertRangeAsync(userRoleEntities);
                 }
             });
        }
    }
}
