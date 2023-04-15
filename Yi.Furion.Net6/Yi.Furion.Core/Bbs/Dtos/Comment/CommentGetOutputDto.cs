using System;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Rbac.Dtos.User;

namespace Yi.Furion.Core.Bbs.Dtos.Comment
{
    /// <summary>
    /// 单返回，返回单条评论即可
    /// </summary>
    public class CommentGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }

        public DateTime? CreateTime { get; set; }
        public string Content { get; set; }

        public long DiscussId { get; set; }


        /// <summary>
        /// 用户id联表为用户对象
        /// </summary>

        public UserGetOutputDto User { get; set; }
        /// <summary>
        /// 根节点的评论id
        /// </summary>
        public long RootId { get; set; }

        /// <summary>
        /// 被回复的CommentId
        /// </summary>
        public long ParentId { get; set; }

    }
}
