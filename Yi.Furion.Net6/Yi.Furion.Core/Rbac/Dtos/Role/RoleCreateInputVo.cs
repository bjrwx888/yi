using System.Collections.Generic;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Dtos.Role
{
    /// <summary>
    /// Role输入创建对象
    /// </summary>
    public class RoleCreateInputVo
    {
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public string Remark { get; set; }
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.ALL;
        public bool State { get; set; } = true;

        public int OrderNum { get; set; }

        public List<long> DeptIds { get; set; }

        public List<long> MenuIds { get; set; }
    }
}
