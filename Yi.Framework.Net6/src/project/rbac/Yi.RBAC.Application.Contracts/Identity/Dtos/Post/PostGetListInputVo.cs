using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.RBAC.Application.Contracts.Identity.Dtos
{
    public class PostGetListInputVo : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public long? CreatorId { get; set; }
        public bool State { get; set; }
        public string PostCode { get; set; }=string.Empty;
        public string PostName { get; set; } = string.Empty;
        public string? Remark { get; set; }
    }
}
