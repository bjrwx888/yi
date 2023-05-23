using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Rbac.Dtos.Role
{
    public class RoleAuthUserGetListInput: PagedAndSortedResultRequestDto
    {
        public string? UserName { get; set; }

        public long? Phone { get; set; }
    }
}
