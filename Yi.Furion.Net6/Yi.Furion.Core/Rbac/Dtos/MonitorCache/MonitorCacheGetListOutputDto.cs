using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Core.Rbac.Dtos.MonitorCache
{
    public class MonitorCacheGetListOutputDto
    {
        public string CacheName { get; set; }
        public string CacheKey { get; set; }
        public string CacheValue { get; set; }
        public string? Remark { get; set; }
    }
}
