using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Domain.Shared.Logs;

namespace Yi.RBAC.Application.Contracts.Logs.Dtos
{
    public class OperationLogGetListInputVo : PagedAllResultRequestDto
    {
        public OperEnum? OperType { get; set; }
        public string? OperUser { get; set; }
    }
}
