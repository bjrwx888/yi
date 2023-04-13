using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Rbac.Application.System.Dtos.Post
{
    public class PostGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public long? CreatorId { get; set; }
        public bool State { get; set; }
        public string PostCode { get; set; } = string.Empty;
        public string PostName { get; set; } = string.Empty;
        public string Remark { get; set; }

        public int OrderNum { get; set; }
    }
}
