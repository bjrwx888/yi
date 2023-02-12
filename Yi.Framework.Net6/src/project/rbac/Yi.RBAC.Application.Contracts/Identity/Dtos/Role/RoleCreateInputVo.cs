using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.RBAC.Domain.Shared.Identity.EnumClasses;

namespace Yi.RBAC.Application.Contracts.Identity.Dtos
{
    /// <summary>
    /// Role输入创建对象
    /// </summary>
    public class RoleCreateInputVo
    {
        public string? RoleName { get; set; }
        public string? RoleCode { get; set; }
        public string? Remark { get; set; }
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.ALL;
        public bool State { get; set; } = true;

        public int OrderNum { get; set; }

        public List<long> DeptIds { get; set; }

        public List<long> MenuIds { get; set; }
    }
}
