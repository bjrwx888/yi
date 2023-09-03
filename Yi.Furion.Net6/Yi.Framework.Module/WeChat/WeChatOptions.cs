using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.WeChat
{
    public class WeChatOptions
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }

        public string PaySecretKey { get; set; }

        public string Mchid { get; set; }

        public string NotifyUrl { get; set; }

        public string MerchantSerialNo { get; set; }

        /// <summary>
        /// 公众号
        /// </summary>
        public OfficialAccountsOptions OfficialAccounts { get; set; }
    }

    public class OfficialAccountsOptions {
        public string AppId { get; set; }
        public string TemplateId { get; set; }
    }
}
