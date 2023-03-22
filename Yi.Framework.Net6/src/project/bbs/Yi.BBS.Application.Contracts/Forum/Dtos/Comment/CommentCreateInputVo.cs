using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    /// <summary>
    /// Comment输入创建对象
    /// </summary>
    public class CommentCreateInputVo
    {

        /// <summary>
        /// 评论id
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 主题id
        /// </summary>
        public long DiscussId { get; set; }

        /// <summary>
        /// 第一层评论id，第一层为0
        /// </summary>
        public long RootId { get; set; }

        /// <summary>
        /// 被回复的CommentId，第一层为0
        /// </summary>
        public long ParentId { get; set; }
    }
}
