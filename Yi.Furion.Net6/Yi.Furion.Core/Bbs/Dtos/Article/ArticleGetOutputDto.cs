using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Core.Bbs.Dtos.Article
{
    public class ArticleGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public long DiscussId { get; set; }
        public long ParentId { get; set; }
    }
}
