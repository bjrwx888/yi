using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Application.Rbac.Dtos.Dept
{
    public class DeptGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public long? CreatorId { get; set; }
        public bool State { get; set; }
        public string DeptName { get; set; } = string.Empty;
        public string DeptCode { get; set; } = string.Empty;
        public string Leader { get; set; }
        public long ParentId { get; set; }
        public string Remark { get; set; }

        public int OrderNum { get; set; }
    }
}
