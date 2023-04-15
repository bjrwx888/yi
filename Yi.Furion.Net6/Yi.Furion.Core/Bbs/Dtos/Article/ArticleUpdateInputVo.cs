namespace Yi.Furion.Core.Bbs.Dtos.Article
{
    public class ArticleUpdateInputVo
    {
        public string Content { get; set; }
        public string Name { get; set; }
        public long DiscussId { get; set; }
        public long ParentId { get; set; }
    }
}
