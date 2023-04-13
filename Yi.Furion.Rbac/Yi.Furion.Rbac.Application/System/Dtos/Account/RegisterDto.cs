using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Rbac.Application.System.Dtos.Account
{
    public class RegisterDto
    {

        //电话号码，根据code的表示来获取

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 唯一标识码
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public long Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}
