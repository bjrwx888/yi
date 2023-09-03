using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.WeChat.Model
{
    public class UnlimitedQRCodeReponse
    {

    }

    public class UnlimitedQRCodeRequest
    {
        public UnlimitedQRCodeRequest(EnvVersionEnum unlimitedQRCodeEnum) {
            env_version = unlimitedQRCodeEnum.ToString();
        }

        public bool check_path { get; set; } = false;
        public string page { get; set; }

        public string scene { get; set; }
        public string env_version { get; set; }
    }

    public enum EnvVersionEnum
    {
        /// <summary>
        /// 正式版本
        /// </summary>
        [Description("正式版本")]
        release,

        /// <summary>
        /// 体验版本
        /// </summary>
        [Description("体验版本")]
        trial,

        /// <summary>
        /// 开发版本
        /// </summary>
        [Description("开发版本")]
        develop
    }
}
