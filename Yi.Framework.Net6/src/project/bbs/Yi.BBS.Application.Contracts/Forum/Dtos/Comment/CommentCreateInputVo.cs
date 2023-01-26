using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    /// <summary>
    /// Comment输入创建对象
    /// </summary>
    public class CommentCreateInputVo
    {
        public long Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Content { get; set; }
        public long DiscussId { get; set; }
        public long UserId { get; set; }
    }
}
