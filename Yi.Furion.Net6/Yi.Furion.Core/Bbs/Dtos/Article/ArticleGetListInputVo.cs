using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Bbs.Dtos.Article
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
