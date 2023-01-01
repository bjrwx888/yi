using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.DTOModel.Base.Dto
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Uuid { get; set; }

        public string Code { get; set; }
    }
}
