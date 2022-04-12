using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IUserService
    {
        /// <summary>
        /// 测试仓储的上下文对象
        /// </summary>
        /// <returns></returns>
        public Task<List<UserEntity>> DbTest();

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="userAction"></param>
        /// <returns></returns>
        public Task<bool> Login(string userName, string password, Action<UserEntity> userAction = null);

        /// <summary>
        /// 注册方法
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userAction"></param>
        /// <returns></returns>
        public Task<bool> Register(UserEntity userEntity, Action<UserEntity> userAction = null);
    }
}
