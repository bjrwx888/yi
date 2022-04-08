using SqlSugar;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class UserService
    {
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
            var user = await _repository.GetFirstAsync(u=>u.UserName== userName);
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
        public async Task<bool> Login(string userName, string password,Action<UserEntity> userAction = null)
        {
           var user=new UserEntity();
            if (await Exist(userName, o => user = o))
            {
                userAction.Invoke(user);
                if (user.Password== Common.Helper.MD5Helper.SHA2Encode(password, user.Salt))
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
                user.UserName= userEntity.UserName;
                user.Salt = Common.Helper.MD5Helper.GenerateSalt();
                user.Password = Common.Helper.MD5Helper.SHA2Encode(userEntity.Password,user.Salt);
                userAction.Invoke(await _repository.InsertReturnEntityAsync(user));
                return true;
            }
            return false;
        }
    }
}
