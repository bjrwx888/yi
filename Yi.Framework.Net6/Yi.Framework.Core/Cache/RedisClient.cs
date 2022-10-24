using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Yi.Framework.Common.IOCOptions;
using CSRedis;

namespace Yi.Framework.Core
{
    public class RedisClient: CacheInvoker
    {
        private readonly RedisConnOptions _RedisOptions;

        private CSRedisClient _client;

        public RedisClient(IOptionsMonitor<RedisConnOptions> redisConnOptions):base(redisConnOptions)
        {
            this._RedisOptions = redisConnOptions.CurrentValue;
            _client = new CSRedisClient($"{_RedisOptions.Host}:{_RedisOptions.Prot},password={_RedisOptions.Password},defaultDatabase ={ _RedisOptions.DB }");
        }

        public override T Get<T>(string key)
        {
            return _client.Get<T>(key);
        }

        public override bool Set<T>(string key, T data, TimeSpan time)
        {
            return _client.Set(key, data, time);
        }

        public override bool Set<T>(string key, T data)
        {
            return _client.Set(key, data);
        }
    }
}
