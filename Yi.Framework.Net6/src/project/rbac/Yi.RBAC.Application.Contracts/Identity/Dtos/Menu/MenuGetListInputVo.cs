using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Domain.Shared.Identity.EnumClasses;

namespace Yi.RBAC.Application.Contracts.Identity.Dtos
{
    public class MenuGetListInputVo : PagedAndSortedResultRequestDto
    {

        public bool? State { get; set; }
        public string? MenuName { get; set; } 
 
    }
}
