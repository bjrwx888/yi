using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    public class ArticleGetListInputVo : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public long DiscussId { get; set; }
        public long ParentId { get; set; }
    }
}
