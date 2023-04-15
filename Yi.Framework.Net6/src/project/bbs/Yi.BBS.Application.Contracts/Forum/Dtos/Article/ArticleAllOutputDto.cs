using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    public class ArticleAllOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }

        //批量查询，不给内容，性能考虑
        //public string Content { get; set; }
        public string Name { get; set; }
        public long DiscussId { get; set; }
        public long ParentId { get; set; }

        public List<ArticleAllOutputDto> Children { get; set; }
    }
}
