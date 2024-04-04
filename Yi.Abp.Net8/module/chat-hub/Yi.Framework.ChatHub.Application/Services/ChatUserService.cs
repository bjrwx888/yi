using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeRedis;
using Mapster;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Yi.Framework.ChatHub.Application.Contracts.Dtos;
using Yi.Framework.ChatHub.Domain.Shared.Caches;

namespace Yi.Framework.ChatHub.Application.Services
{
    public class ChatUserService : ApplicationService
    {
        /// <summary>
        /// 使用懒加载防止报错
        /// </summary>
        private IRedisClient RedisClient => LazyServiceProvider.LazyGetRequiredService<IRedisClient>();
        private string CacheKeyPrefix => LazyServiceProvider.LazyGetRequiredService<IOptions<AbpDistributedCacheOptions>>().Value.KeyPrefix;
        public async Task<List<ChatUserGetListOutputDto>> GetListAsync()
        {
            var key = new ChatOnlineUserCacheKey(CacheKeyPrefix);
            var cacheUsers = (await RedisClient.HGetAllAsync(key.GetKey())).Select(x => System.Text.Json.JsonSerializer.Deserialize < ChatOnlineUserCacheItem >( x.Value)).ToList();
            var output = cacheUsers.Adapt<List<ChatUserGetListOutputDto>>();
            return output;
        }
    }
}
