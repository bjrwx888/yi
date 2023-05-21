using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Dtos.Role
{
    public class UpdateDataScpoceInput
    {
        public long RoleId { get; set; }

        public List<long>? DeptIds { get; set;}

        public DataScopeEnum DataScope { get; set; }
    }
}
