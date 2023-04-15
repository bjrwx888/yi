using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Rbac.Dtos.User
{
    public class UserGetListInputVo : PagedAllResultRequestDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public long? Phone { get; set; }

        public bool? State { get; set; }

        public long? DeptId { get; set; }

        public string Ids { get; set; }

    }
}
