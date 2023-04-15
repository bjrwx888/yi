using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Auditing;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.BBS.Domain.Exhibition.Entities
{
    [SugarTable("Banner")]
    public class BannerEntity : IEntity<long>, ISoftDelete, IAuditedObject
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Color { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

        public long? CreatorId { get; set; }

        public long? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
