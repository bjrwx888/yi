using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.RBAC.Application.Contracts.Setting.Dtos
{
    public class ConfigGetListInputVo : PagedAllResultRequestDto
    {
        public string? ConfigName { get; set; }
        public string? ConfigKey { get; set; } 
        public DateTime CreationTime { get; set; }
    }
}
