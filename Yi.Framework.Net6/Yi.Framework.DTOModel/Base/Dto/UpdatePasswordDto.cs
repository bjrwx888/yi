using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.DTOModel.Base.Dto
{
    public class UpdatePasswordDto
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
