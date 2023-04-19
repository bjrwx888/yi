using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;

namespace Yi.Framework.Module.Caching
{
    public class MemoryCacheClient : CacheManager,ISingleton
    {
        private IMemoryCache Client { get; set; }
        public MemoryCacheClient()
        {
            Client = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
        }
    }
}
