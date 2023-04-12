using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Data.Auditing;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.BBS.Domain.Exhibition.Entities
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
