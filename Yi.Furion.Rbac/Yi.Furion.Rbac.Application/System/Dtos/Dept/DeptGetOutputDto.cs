using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Rbac.Application.System.Dtos.Dept
{
    public class DeptGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public bool State { get; set; }
        public string DeptName { get; set; } = string.Empty;
        public string DeptCode { get; set; } = string.Empty;
        public string Leader { get; set; }
        public string Remark { get; set; }

        public long? deptId { get; set; }

        public int OrderNum { get; set; }

        public long ParentId { get; set; }
    }
}
