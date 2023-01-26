using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.BBS.Domain.Forum.Entities
{
    [SugarTable("Comment")]
    public class CommentEntity : IEntity<long>, ISoftDelete
    {
        public CommentEntity()
        { 
        }

        internal CommentEntity(long discussId, long userId)
        {
            DiscussId= discussId;
            UserId = userId;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? CreateTime { get; set; }
        public string Content { get; set; }

        public long DiscussId { get; set; }
        public long UserId { get; set; }
    }
}
