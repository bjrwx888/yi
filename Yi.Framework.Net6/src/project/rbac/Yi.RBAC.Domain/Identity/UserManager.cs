using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Exceptions;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Shared.Identity.ConstClasses;

namespace Yi.RBAC.Domain.Identity
{

    /// <summary>
    /// 用户领域服务
    /// </summary>
    [AppService]
    public class UserManager
    {
        private readonly IRepository<UserEntity> _repository;
        public UserManager(IRepository<UserEntity> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 登录效验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="userAction"></param>
        /// <returns></returns>
        public async Task LoginValidationAsync(string userName, string password, Action<UserEntity>? userAction = null)
        {
            var user = new UserEntity();
            if (await ExistAsync(userName, o => user = o))
            {
                if (userAction is not null)
                {
                    userAction.Invoke(user);
                }
                if (user.Password != MD5Helper.SHA2Encode(password, user.Salt))
                {
                    return;
                }
                throw new UserFriendlyException(UserConst.登录失败_错误);
            }
            throw new UserFriendlyException(UserConst.登录失败_不存在);
        }

        /// <summary>
        /// 判断账户合法存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userAction"></param>
        /// <returns></returns>
        public async Task<bool> ExistAsync(string userName, Action<UserEntity>? userAction = null)
        {
            var user = await _repository.GetFirstAsync(u => u.UserName == userName && u.IsDeleted == false);
            if (userAction is not null)
            {
                userAction.Invoke(user);
            }
            if (user == null)
            {
                return false;
            }
            return true;
        }

    }
}
