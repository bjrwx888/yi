using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Auditing;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;
using Yi.RBAC.Domain.Identity.Entities;

namespace Yi.BBS.Domain.Forum.Entities
{

    /// <summary>
    /// 评论表
    /// </summary>
    [SugarTable("Comment")]
    public class CommentEntity : IEntity<long>, ISoftDelete,IAuditedObject
    {
        /// <summary>
        /// 采用二维数组方式，不使用树形方式
        /// </summary>
        public CommentEntity()
        { 
        }

        internal CommentEntity(long discussId)
        {
            DiscussId= discussId;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }

        public long DiscussId { get; set; }

        /// <summary>
        /// 被回复的CommentId
        /// </summary>
        public long ParentId { get; set; }
        public DateTime CreationTime { get; set; }

        public long RootId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<CommentEntity> Children { get; set; } = new();


        /// <summary>
        /// 用户,评论人用户信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CreatorId))]
        public UserEntity CreateUser { get; set; }

        /// <summary>
        /// 被评论的用户信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public UserEntity CommentedUser { get; set; }

        public long? CreatorId { get; set; }

        public long? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
