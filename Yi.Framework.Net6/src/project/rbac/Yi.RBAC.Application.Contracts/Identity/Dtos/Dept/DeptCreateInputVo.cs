using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.RBAC.Application.Contracts.Identity.Dtos
{
    /// <summary>
    /// Dept输入创建对象
    /// </summary>
    public class DeptCreateInputVo
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public long? CreatorId { get; set; }
        public bool State { get; set; }
        public string DeptName { get; set; }=string.Empty;
        public string DeptCode { get; set; } = string.Empty;
        public string? Leader { get; set; }
        public long ParentId { get; set; }
        public string? Remark { get; set; }
    }
}
