using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Users;
using Yi.Framework.GoView.Application.Contracts.Dtos;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.Rbac.Domain.Managers;
using Yi.Framework.Rbac.Domain.Shared.Caches;

namespace Yi.Framework.GoView.Application.Services
{
    public class GoViewSystemService : ApplicationService
    {
        private readonly  IAccountManager _accountManager;
        private readonly ICurrentUser _currentUser;
        private readonly IDistributedCache<UserInfoCacheItem, UserInfoCacheKey> _userCache;


        public GoViewSystemService(IAccountManager accountManager, ICurrentUser currentUser, IDistributedCache<UserInfoCacheItem, UserInfoCacheKey> userCache)
        {
            _accountManager = accountManager;
            _currentUser = currentUser;
            _userCache = userCache;
        }

        /// <summary>
        /// GoView 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<GoViewLoginOutput> PostLoginAsync(GoViewLoginInput input)
        {
            if (string.IsNullOrEmpty(input.Password) || string.IsNullOrEmpty(input.Username))
            {
                throw new UserFriendlyException("请输入合理数据！");
            }

            UserAggregateRoot user = new();

            //校验
            await _accountManager.LoginValidationAsync(input.Username, input.Password, x => user = x);

            //获取token
            var accessToken = await _accountManager.GetTokenByUserIdAsync(user.Id);
            var refreshToken = _accountManager.CreateRefreshToken(user.Id);

            return new GoViewLoginOutput()
            {
                Userinfo = new GoViewLoginUserInfo
                {
                    Id = user.Id.ToString(),
                    Username = user.UserName,
                    Nickname = user.Name ?? "未配置昵称",
                },
                Token = new GoViewLoginToken
                {
                    TokenValue = $"Bearer {accessToken}"
                }
            };
        }

        /// <summary>
        /// GoView 退出
        /// </summary>
        [AllowAnonymous]
        public async Task<bool> GetLogout()
        {
            //通过鉴权jwt获取到用户的id
            var userId = _currentUser.Id;
            if (userId is null)
            {
                return false;
            }

            await _userCache.RemoveAsync(new UserInfoCacheKey(userId.Value));
            //Jwt去中心化登出，只需用记录日志即可
            return true;
        }

        /// <summary>
        /// 获取 OSS 上传接口
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("go-view-system/getOssInfo")]
        public Task<GoViewOssUrlOutput> GetOssInfoAsync()
        {
            return Task.FromResult(new GoViewOssUrlOutput { BucketURL = "" });
        }
    }
}
