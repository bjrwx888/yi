using Mapster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Core.Exceptions;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Shared.Identity.ConstClasses;

namespace Yi.RBAC.Domain.Identity
{

    /// <summary>
    /// 用户领域服务
    /// </summary>
    [AppService]
    public class AccountManager
    {
        private readonly IRepository<UserEntity> _repository;
        public AccountManager(IRepository<UserEntity> repository)
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
                if (user.Password == MD5Helper.SHA2Encode(password, user.Salt))
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
            var user = await _repository.GetFirstAsync(u => u.UserName == userName && u.State == true);
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

        /// <summary>
        /// 令牌转换
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        public Dictionary<string, object> UserInfoToClaim(UserRoleMenuDto dto)
        {
            var claims = new Dictionary<string, object>();
            claims.Add(nameof(ICurrentUser.Id), dto.User.Id);
            claims.Add(nameof(ICurrentUser.UserName), dto.User.UserName);
            if (dto.User.Email is not null)
            {
                claims.Add(nameof(ICurrentUser.Email), dto.User.Email);
            }
            if (dto.User.Phone is not null)
            {
                claims.Add(nameof(ICurrentUser.PhoneNumber), dto.User.Phone);
            }
            if (UserConst.Admin.Equals(dto.User.UserName))
            {
                claims.Add(nameof(ICurrentUser.Permission), UserConst.AdminPermissionCode);
            }
            else
            {
                claims.Add(nameof(ICurrentUser.Permission), dto.PermissionCodes.Where(x => !string.IsNullOrEmpty(x)));
            }

            return claims;
        }

        public async Task UpdatePasswordAsync(long userId, string newPassword, string oldPassword)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (!user.JudgePassword(oldPassword))
            {
                throw new UserFriendlyException("无效更新！新密码不能与老密码相同");
            }
            user.Password = newPassword;
            user.BuildPassword();
            await _repository.UpdateAsync(user);
        }


        public async Task<bool> RestPasswordAsync(long userId, string password)
        {
            var user = await _repository.GetByIdAsync(userId);
            user.Id = userId;
            user.Password = password;
            user.BuildPassword();
            return await _repository.UpdateAsync(user);


        }
    }

}
