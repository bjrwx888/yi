using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;
using Volo.Abp.Security.Claims;
using Yi.Framework.Core.Helper;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.Rbac.Domain.Shared.Consts;
using Yi.Framework.Rbac.Domain.Shared.Dtos;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Rbac.Domain.Managers
{

    /// <summary>
    /// 用户领域服务
    /// </summary>
    public class AccountManager : DomainService
    {
        private readonly ISqlSugarRepository<UserEntity> _repository;
        public AccountManager(ISqlSugarRepository<UserEntity> repository)
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
        public async Task LoginValidationAsync(string userName, string password, Action<UserEntity> userAction = null)
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
                throw new UserFriendlyException(UserConst.Login_Error);
            }
            throw new UserFriendlyException(UserConst.Login_User_No_Exist);
        }

        /// <summary>
        /// 判断账户合法存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userAction"></param>
        /// <returns></returns>
        public async Task<bool> ExistAsync(string userName, Action<UserEntity> userAction = null)
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
            claims.Add(AbpClaimTypes.UserId, dto.User.Id);
            claims.Add(AbpClaimTypes.UserName, dto.User.UserName);
            if (dto.User.DeptId is not null)
            {
                claims.Add(TokenTypeConst.DeptId, dto.User.DeptId);
            }
            if (dto.User.Email is not null)
            {
                claims.Add(AbpClaimTypes.Email, dto.User.Email);
            }
            if (dto.User.Phone is not null)
            {
                claims.Add(AbpClaimTypes.PhoneNumber, dto.User.Phone);
            }
            if (UserConst.Admin.Equals(dto.User.UserName))
            {
                claims.Add(TokenTypeConst.Permission, UserConst.AdminPermissionCode);
                claims.Add(TokenTypeConst.Roles, UserConst.AdminRolesCode);
            }
            else
            {
                claims.Add(TokenTypeConst.Permission, dto.PermissionCodes.Where(x => !string.IsNullOrEmpty(x)));
                claims.Add(AbpClaimTypes.Role, dto.RoleCodes.Where(x => !string.IsNullOrEmpty(x)));
            }

            return claims;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task UpdatePasswordAsync(Guid userId, string newPassword, string oldPassword)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (!user.JudgePassword(oldPassword))
            {
                throw new UserFriendlyException("无效更新！原密码错误！");
            }
            user.Password = newPassword;
            user.BuildPassword();
            await _repository.UpdateAsync(user);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> RestPasswordAsync(Guid userId, string password)
        {
            var user = await _repository.GetByIdAsync(userId);
            EntityHelper.TrySetId(user, () => GuidGenerator.Create(), true);
            user.Password = password;
            user.BuildPassword();
            return await _repository.UpdateAsync(user);
        }
    }

}
