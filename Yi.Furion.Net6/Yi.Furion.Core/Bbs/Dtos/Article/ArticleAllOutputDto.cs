using System.Collections.Generic;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Core.Bbs.Dtos.Article
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
