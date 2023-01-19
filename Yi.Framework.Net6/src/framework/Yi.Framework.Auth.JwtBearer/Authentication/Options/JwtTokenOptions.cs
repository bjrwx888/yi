using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Auth.JwtBearer.Authentication.Options
{
    public class JwtTokenOptions
    {
        /// <summary>
        /// 听众
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// 发行者
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 过期时间，单位秒
        /// </summary>
        public long ExpSecond { get; set; }
    }
}
