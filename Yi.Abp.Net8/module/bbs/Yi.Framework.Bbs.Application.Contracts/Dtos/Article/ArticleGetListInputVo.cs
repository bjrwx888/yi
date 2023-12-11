using Volo.Abp.Application.Dtos;

namespace Yi.Framework.Bbs.Application.Contracts.Dtos.Article
{
    public class ArticleGetListInputVo : PagedAndSortedResultRequestDto
    {
        public string? Content { get; set; }
        public string? Name { get; set; }
        public Guid? DiscussId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
