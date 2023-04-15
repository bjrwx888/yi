using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Rbac.Application.System.Dtos.Dept
{
    public class DeptGetListInputVo : PagedAllResultRequestDto
    {
        public long Id { get; set; }
        public bool? State { get; set; }
        public string DeptName { get; set; }
        public string DeptCode { get; set; }
        public string Leader { get; set; }

    }
}
