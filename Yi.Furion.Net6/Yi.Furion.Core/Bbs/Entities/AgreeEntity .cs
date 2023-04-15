using System;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Ddd.Entities;
using Yi.Framework.Infrastructure.Helper;

namespace Yi.Furion.Core.Bbs.Entities
{
    [SugarTable("Agree")]
    public class AgreeEntity : IEntity<long>, ICreationAuditedObject
    {
        public AgreeEntity()
        {
        }

        public AgreeEntity(long discussId)
        {
            DiscussId = discussId;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; } = SnowflakeHelper.NextId;
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 主题id
        /// </summary>
        public long DiscussId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public long? CreatorId { get; set; }
    }
}
