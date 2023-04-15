using System;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Core.Bbs.Entities
{
    [SugarTable("Banner")]
    public class BannerEntity : IEntity<long>, ISoftDelete, IAuditedObject
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Color { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

        public long? CreatorId { get; set; }

        public long? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
