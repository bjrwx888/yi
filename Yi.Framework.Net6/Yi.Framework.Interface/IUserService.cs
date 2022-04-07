using System;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IUserService
    {
        public Task<bool> Login(string userName, string password, Action<UserEntity> userAction = null);
        public Task<bool> Register(UserEntity userEntity, Action<UserEntity> userAction = null);
    }
}
