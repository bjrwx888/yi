using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.RBAC.Application.Contracts.Dictionary.Dtos
{
    public class DictionaryTypeGetListInputVo : PagedAllResultRequestDto
    {
        public string? DictName { get; set; } 
        public string? DictType { get; set; }
        public string? Remark { get; set; }

        public bool? State { get; set; }
    }
}
