using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Application.Contracts.Identity.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
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
