using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.RBAC.Application.Contracts.Identity.Dtos
{
    public class DeptGetListInputVo : PagedAllResultRequestDto
    {
        public long Id { get; set; }
        public bool? State { get; set; }
        public string? DeptName { get; set; }
        public string? DeptCode { get; set; } 
        public string? Leader { get; set; }
    }
}
