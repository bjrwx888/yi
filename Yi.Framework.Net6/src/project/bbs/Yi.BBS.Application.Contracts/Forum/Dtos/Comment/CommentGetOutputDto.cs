using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    public class CommentGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Content { get; set; }
        public long DiscussId { get; set; }
        public long UserId { get; set; }
    }
}
