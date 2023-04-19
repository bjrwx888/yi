using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Yi.Furion.Core.Rbac.Dtos.MonitorCache;

namespace Yi.Furion.Application.Rbac.Services.Impl
{
    public class MonitorCacheService : IMonitorCacheService,IDynamicApiController,ITransient
    {
        private IMemoryCache _memoryCache;
        public MonitorCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        //cacheKey value为空，只要name和备注

        public List<MonitorCacheNameGetListOutputDto> GetName()
        {
            return new List<MonitorCacheNameGetListOutputDto>() {
            new MonitorCacheNameGetListOutputDto{ CacheName="Yi:Login",Remark="登录验证码"},
             new MonitorCacheNameGetListOutputDto{ CacheName="Yi:User",Remark="用户信息"}
            };
        }
        [HttpGet("key/{cacaheName}")]
        public List<string> GetKey([FromRoute] string cacaheName)
        {
            return new List<string>() { "1233124","3124","1231251","12312412"};
        }

        //全部不为空
        [HttpGet("value/{cacaheName}/{cacaheKey}")]
        public MonitorCacheGetListOutputDto GetValue([FromRoute] string cacaheName, [FromRoute] string cacaheKey)
        {
            return new MonitorCacheGetListOutputDto() { CacheKey= "1233124",CacheName= "Yi:Login",CacheValue="ttt",Remark= "登录验证码" };
        }
    }


}
