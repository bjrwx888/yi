using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Caching.MemoryCache
{
    public class MemoryCacheClient : CacheManager
    {
        private IMemoryCache _client;
        public MemoryCacheClient()
        {
            _client = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
        }
        public override bool Exits(string key)
        {
            return _client.TryGetValue(key, out var _);
        }
        public override T Get<T>(string key)
        {
            return _client.Get<T>(key);
        }
        public override bool Set<T>(string key, T item)
        {
            return _client.Set(key, item) is not null;
        }

        public override bool Set<T>(string key, T item, TimeSpan time)
        {
            return _client.Set(key, item, time) is not null;
        }

        public override long Del(string key)
        {
            _client.Remove(key);
            return 1;
        }
    }
}
