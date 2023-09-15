using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Helper;

namespace Yi.Furion.Core.Bbs.Dtos.AccessLog
{
    public class AccessLogDto
    {
        public long Id { get; set; } 
        public long Number { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
