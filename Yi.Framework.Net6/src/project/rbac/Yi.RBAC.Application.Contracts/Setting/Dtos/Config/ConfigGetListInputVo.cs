using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.RBAC.Application.Contracts.Setting.Dtos
{
    /// <summary>
    /// ≈‰÷√≤È—Ø≤Œ ˝
    /// </summary>
    public class ConfigGetListInputVo : PagedAllResultRequestDto
    {
        /// <summary>
        /// ≈‰÷√√˚≥∆
        /// </summary>
        public string? ConfigName { get; set; }

        /// <summary>
        /// ≈‰÷√º¸
        /// </summary>
        public string? ConfigKey { get; set; } 

    }
}
