using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Rbac.Application.System.Dtos.Role
{
    public class RoleGetListInputVo : PagedAllResultRequestDto
    {
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public bool? State { get; set; }

    }
}
