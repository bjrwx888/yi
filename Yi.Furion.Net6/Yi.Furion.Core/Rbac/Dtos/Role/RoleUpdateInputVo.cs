using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Dtos.Role
{
    public class RoleUpdateInputVo
    {
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public string Remark { get; set; }
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.ALL;
        public bool State { get; set; }

        public int OrderNum { get; set; }

        public List<long> DeptIds { get; set; }

        public List<long> MenuIds { get; set; }
    }
}
