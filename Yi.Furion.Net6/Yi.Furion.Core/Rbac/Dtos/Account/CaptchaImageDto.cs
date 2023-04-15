using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Core.Rbac.Dtos.Account
{
    public class CaptchaImageDto
    {
        public string Code { get; set; } = string.Empty;
        public Guid Uuid { get; set; } = Guid.Empty;
        public byte[] Img { get; set; }
    }
}
