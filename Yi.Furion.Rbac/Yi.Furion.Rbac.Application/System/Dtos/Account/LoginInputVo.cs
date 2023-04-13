using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Rbac.Application.System.Dtos.Account
{
    public class LoginInputVo
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Uuid { get; set; }

        public string Code { get; set; }
    }
}
