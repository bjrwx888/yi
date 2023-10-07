using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Core.App.Dtos.Trends
{
    public class TrendsGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long UserId { get; set; }
        public bool IsDeleted { get; set; }
        public string? Remark { get; set; }
        public List<long>? Images { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorId { get; set; }
        public long? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }


    }
}
