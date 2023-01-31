using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Domain.Shared.Identity.EnumClasses;

namespace Yi.RBAC.Application.Contracts.Identity.Dtos
{
    public class UserGetListInputVo : PagedAllResultRequestDto
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public long? Phone { get; set; }

        public bool IsDeleted { get; set; }

    }
}
