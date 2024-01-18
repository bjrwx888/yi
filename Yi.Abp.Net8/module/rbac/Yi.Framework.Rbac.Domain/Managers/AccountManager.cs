﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TencentCloud.Tdmq.V20200217.Models;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Security.Claims;
using Yi.Framework.Core.Helper;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.Rbac.Domain.Repositories;
using Yi.Framework.Rbac.Domain.Shared.Consts;
using Yi.Framework.Rbac.Domain.Shared.Dtos;
using Yi.Framework.Rbac.Domain.Shared.Etos;
using Yi.Framework.Rbac.Domain.Shared.Model;
using Yi.Framework.Rbac.Domain.Shared.Options;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Rbac.Domain.Managers
{

    /// <summary>
    /// 用户领域服务
    /// </summary>
    public class AccountManager : DomainService, IAccountManager
    {
        private readonly IUserRepository _repository;
        private readonly ILocalEventBus _localEventBus;
        private readonly JwtOptions _jwtOptions;
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager _userManager;
        private ISqlSugarRepository<RoleEntity> _roleRepository;
        public AccountManager(IUserRepository repository
            , IHttpContextAccessor httpContextAccessor
            , IOptions<JwtOptions> jwtOptions
            , ILocalEventBus localEventBus
            , UserManager userManager
            , ISqlSugarRepository<RoleEntity> roleRepository)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions.Value;
            _localEventBus = localEventBus;
            _userManager = userManager;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 根据用户id获取token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<string> GetTokenByUserIdAsync(Guid userId)
        {
            //获取用户信息
            var userInfo = await _repository.GetUserAllInfoAsync(userId);

            //判断用户状态
            if (userInfo.User.State == false)
            {
                throw new UserFriendlyException(UserConst.State_Is_State);
            }

            if (userInfo.RoleCodes.Count == 0)
            {
                throw new UserFriendlyException(UserConst.No_Role);
            }
            //这里抛出一个登录的事件
            if (_httpContextAccessor.HttpContext is not null)
            {
                var loginEntity = new LoginLogEntity().GetInfoByHttpContext(_httpContextAccessor.HttpContext);
                var loginEto = loginEntity.Adapt<LoginEventArgs>();
                loginEto.UserName = userInfo.User.UserName;
                loginEto.UserId = userInfo.User.Id;
                await _localEventBus.PublishAsync(loginEto);
            }
            //将用户信息添加到缓存中，需要考虑的是更改了用户、角色、菜单等整个体系都需要将缓存进行刷新，看具体业务进行选择

            var accessToken = CreateToken(this.UserInfoToClaim(userInfo));
            return accessToken;
        }

        /// <summary>
        /// 创建令牌
        /// </summary>
        /// <param name="kvs"></param>
        /// <returns></returns>
        private string CreateToken(List<KeyValuePair<string, string>> kvs)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = kvs.Select(x => new Claim(x.Key, x.Value.ToString())).ToList();
            var token = new JwtSecurityToken(
               issuer: _jwtOptions.Issuer,
               audience: _jwtOptions.Audience,
               claims: claims,
               expires: DateTime.Now.AddSeconds(_jwtOptions.ExpiresMinuteTime),
               notBefore: DateTime.Now,
               signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);

            return returnToken;
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

        public List<KeyValuePair<string, string>> UserInfoToClaim(UserRoleMenuDto dto)
        {
            var claims = new List<KeyValuePair<string, string>>();
            AddToClaim(claims, AbpClaimTypes.UserId, dto.User.Id.ToString());
            AddToClaim(claims, AbpClaimTypes.UserName, dto.User.UserName);
            if (dto.User.DeptId is not null)
            {
                AddToClaim(claims, TokenTypeConst.DeptId, dto.User.DeptId.ToString());
            }
            if (dto.User.Email is not null)
            {
                AddToClaim(claims, AbpClaimTypes.Email, dto.User.Email);
            }
            if (dto.User.Phone is not null)
            {
                AddToClaim(claims, AbpClaimTypes.PhoneNumber, dto.User.Phone.ToString());
            }
            if (dto.Roles.Count > 0)
            {
                AddToClaim(claims, TokenTypeConst.RoleInfo, JsonConvert.SerializeObject(dto.Roles.Select(x => new RoleTokenInfoModel { Id = x.Id, DataScope = x.DataScope })));
            }
            if (UserConst.Admin.Equals(dto.User.UserName))
            {
                AddToClaim(claims, TokenTypeConst.Permission, UserConst.AdminPermissionCode);
                AddToClaim(claims, TokenTypeConst.Roles, UserConst.AdminRolesCode);
            }
            else
            {
                dto.PermissionCodes?.ForEach(per => AddToClaim(claims, TokenTypeConst.Permission, per));
                dto.RoleCodes?.ForEach(role => AddToClaim(claims, AbpClaimTypes.Role, role));
            }

            return claims;
        }


        private void AddToClaim(List<KeyValuePair<string, string>> claims, string key, string value)
        {
            claims.Add(new KeyValuePair<string, string>(key, value));
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


        public async Task RegisterAsync(string userName, string password, long phone)
        {
            //输入的用户名与电话号码都不能在数据库中存在
            UserEntity user = new();
            var isExist = await _repository.IsAnyAsync(x => x.UserName == userName || x.Phone == phone);
            if (isExist)
            {
                throw new UserFriendlyException("用户已存在，注册失败");
            }

            var newUser = new UserEntity(userName, password, phone);

            var entity = await _repository.InsertReturnEntityAsync(newUser);
            //赋上一个初始角色
            var role = await _roleRepository.GetFirstAsync(x => x.RoleCode == UserConst.DefaultRoleCode);
            if (role is not null)
            {
                await _userManager.GiveUserSetRoleAsync(new List<Guid> { entity.Id }, new List<Guid> { role.Id });
            }

            await _localEventBus.PublishAsync(new UserCreateEventArgs(entity.Id));

        }
    }

}
