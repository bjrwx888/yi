using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSRedis;
using Furion.DependencyInjection;
using Microsoft.Extensions.Options;
using static CSRedis.CSRedisClient;

namespace Yi.Framework.Module.Caching
{

    public class RedisCacheClient : CacheManager, ISingleton
    {
        public readonly CachingConnOptions _RedisOptions;

        //公开客户端,csredis封装的很完美了
        public CSRedisClient Client { get; set; }

        public RedisCacheClient(IOptions<CachingConnOptions> redisConnOptions)
        {
            this._RedisOptions = redisConnOptions.Value;
            Client = new CSRedisClient($"{_RedisOptions.Host}:{_RedisOptions.Prot},password={_RedisOptions.Password},defaultDatabase ={_RedisOptions.DB}");
        }
    }
}
