using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Ddd.Entities;
using Yi.Framework.Infrastructure.Helper;

namespace Yi.Furion.Core.Bbs.Entities
{
    [SugarTable("AccessLog")]
    public class AccessLogEntity : IEntity<long>, IHasModificationTime, IHasCreationTime
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; } = SnowflakeHelper.NextId;
        public long Number { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
