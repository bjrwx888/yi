using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.RBAC.Application.Contracts.Identity.Dtos
{
    public class DeptGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public bool State { get; set; }
        public string DeptName { get; set; }=string.Empty;
        public string DeptCode { get; set; } = string.Empty;
        public string? Leader { get; set; }
        public string? Remark { get; set; }

        public long? deptId { get; set; }
    }
}
