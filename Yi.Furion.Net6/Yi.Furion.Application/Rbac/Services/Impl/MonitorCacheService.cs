using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSRedis;
using Furion.ClayObject.Extensions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Memory;
using Yi.Framework.Module.Caching;
using Yi.Furion.Core.Rbac.Dtos.MonitorCache;

namespace Yi.Furion.Application.Rbac.Services.Impl
{
    [ApiDescriptionSettings("RBAC")]
    public class MonitorCacheService : IMonitorCacheService, IDynamicApiController, ITransient
    {
        private static List<MonitorCacheNameGetListOutputDto> monitorCacheNames => new List<MonitorCacheNameGetListOutputDto>()
        {
          new MonitorCacheNameGetListOutputDto{ CacheName="Yi:Login",Remark="登录验证码"},
             new MonitorCacheNameGetListOutputDto{ CacheName="Yi:User",Remark="用户信息"}
        };
        private Dictionary<string, string> monitorCacheNamesDic => monitorCacheNames.ToDictionary(x => x.CacheName, x => x.Remark);
        private CSRedisClient _cacheClient;
        public MonitorCacheService(RedisCacheClient redisCacheClient)
        {
            _cacheClient = redisCacheClient.Client;
        }
        //cacheKey value为空，只要name和备注

        public List<MonitorCacheNameGetListOutputDto> GetName()
        {
            //固定的
            return monitorCacheNames;
        }
        [HttpGet("key/{cacaheName}")]
        public List<string> GetKey([FromRoute] string cacaheName)
        {
            var output = _cacheClient.Keys($"{cacaheName}:*");
            return output.ToList();
        }

        //全部不为空
        [HttpGet("value/{cacaheName}/{cacaheKey}")]
        public MonitorCacheGetListOutputDto GetValue([FromRoute] string cacaheName, [FromRoute] string cacaheKey)
        {
            var value = _cacheClient.Get($"{cacaheName}:{cacaheKey}");
            return new MonitorCacheGetListOutputDto() { CacheKey = cacaheKey, CacheName = cacaheName, CacheValue = "ttt", Remark = monitorCacheNamesDic[cacaheName] };
        }
    }


}
