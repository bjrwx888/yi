using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.RBAC.Application.Contracts.Identity.Dtos.Account
{
    public class CaptchaImageDto
    {
        public string Uuid { get; set; }=string.Empty;
        public byte[] Img { get; set; }
    }
}
