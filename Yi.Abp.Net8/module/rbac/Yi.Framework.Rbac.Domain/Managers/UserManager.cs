using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.Rbac.Domain.Repositories;
using Yi.Framework.Rbac.Domain.Shared.Caches;
using Yi.Framework.Rbac.Domain.Shared.Dtos;
using Yi.Framework.Rbac.Domain.Shared.Options;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Rbac.Domain.Managers
{
    public class UserManager : DomainService
    {
        public readonly ISqlSugarRepository<UserEntity> _repository;
        public readonly ISqlSugarRepository<UserRoleEntity> _repositoryUserRole;
        public readonly ISqlSugarRepository<UserPostEntity> _repositoryUserPost;
        private IDistributedCache<UserInfoCacheItem, UserInfoCacheKey> _userCache;
        private readonly IGuidGenerator _guidGenerator;
        private IUserRepository _userRepository;
        public UserManager(ISqlSugarRepository<UserEntity> repository, ISqlSugarRepository<UserRoleEntity> repositoryUserRole, ISqlSugarRepository<UserPostEntity> repositoryUserPost, IGuidGenerator guidGenerator, IDistributedCache<UserInfoCacheItem, UserInfoCacheKey> userCache, IUserRepository userRepository) =>
            (_repository, _repositoryUserRole, _repositoryUserPost, _guidGenerator, _userCache, _userRepository) =
            (repository, repositoryUserRole, repositoryUserPost, guidGenerator, userCache, userRepository);

        /// <summary>
        /// 给用户设置角色
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task GiveUserSetRoleAsync(List<Guid> userIds, List<Guid> roleIds)
        {
            //删除用户之前所有的用户角色关系（物理删除，没有恢复的必要）
            await _repositoryUserRole.DeleteAsync(u => userIds.Contains(u.UserId));

            if (roleIds is not null)
            {
                //遍历用户
                foreach (var userId in userIds)
                {
                    //添加新的关系
                    List<UserRoleEntity> userRoleEntities = new();

                    foreach (var roleId in roleIds)
                    {
                        userRoleEntities.Add(new UserRoleEntity() { UserId = userId, RoleId = roleId });
                    }
                    //一次性批量添加
                    await _repositoryUserRole.InsertRangeAsync(userRoleEntities);
                }
            }
        }


        /// <summary>
        /// 给用户设置岗位
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="postIds"></param>
        /// <returns></returns>
        public async Task GiveUserSetPostAsync(List<Guid> userIds, List<Guid> postIds)
        {
            //删除用户之前所有的用户角色关系（物理删除，没有恢复的必要）
            await _repositoryUserPost.DeleteAsync(u => userIds.Contains(u.UserId));
            if (postIds is not null)
            {
                //遍历用户
                foreach (var userId in userIds)
                {
                    //添加新的关系
                    List<UserPostEntity> userPostEntities = new();
                    foreach (var post in postIds)
                    {
                        userPostEntities.Add(new UserPostEntity() { UserId = userId, PostId = post });
                    }

                    //一次性批量添加
                    await _repositoryUserPost.InsertRangeAsync(userPostEntities);
                }

            }
        }


        /// <summary>
        /// 查询用户信息，已缓存
        /// </summary>
        /// <returns></returns>
        public async Task<UserRoleMenuDto> Get(Guid userId)
        {
            //if (userId is null)
            //{
            //    throw new UserFriendlyException("用户未登录");
            //}
            //此处优先从缓存中获取
            UserRoleMenuDto output = null;

            var cacheData = await _userCache.GetAsync(new UserInfoCacheKey(userId));
            if (cacheData is not null)
            {
                output = cacheData.Info;
            }
            else
            {
                var data = await _userRepository.GetUserAllInfoAsync(userId);
                //系统用户数据被重置，老前端访问重新授权
                if (data is null)
                {
                    throw new AbpAuthorizationException();
                }
                data.Menus.Clear();

                output = data;

                var tokenExpiresMinuteTime = LazyServiceProvider.GetRequiredService<IOptions<JwtOptions>>().Value.ExpiresMinuteTime;
                //将用户信息放入缓存，下次获取直接从缓存中获取即可，过期时间为token过期时间
                await _userCache.SetAsync(new UserInfoCacheKey(userId), new UserInfoCacheItem(data), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(tokenExpiresMinuteTime) });
            }
            return output;
        }



    }

}