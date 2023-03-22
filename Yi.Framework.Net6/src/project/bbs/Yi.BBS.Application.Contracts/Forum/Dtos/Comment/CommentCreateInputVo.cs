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

        public string Content { get; set; }

        public long DiscussId { get; set; }

        /// <summary>
        /// 根节点的评论id，这里也可根据树形查询获取到根节点，但是不够优雅，前端是二维数组，选择前端传值即可,如果是根，传0，如果不是
        /// </summary>
        public long RootId { get; set; }

        /// <summary>
        /// 被回复的CommentId
        /// </summary>
        public long ParentId { get; set; }
    }
}
