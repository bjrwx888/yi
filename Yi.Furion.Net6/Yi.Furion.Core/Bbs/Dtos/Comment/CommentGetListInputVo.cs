using System;

namespace Yi.Furion.Core.Bbs.Dtos.Comment
{
    public class CommentGetListInputVo
    {
        public DateTime? creationTime { get; set; }
        public string Content { get; set; }

        //应该选择具体莫个主题查询
        public long? DiscussId { get; set; }
    }
}
