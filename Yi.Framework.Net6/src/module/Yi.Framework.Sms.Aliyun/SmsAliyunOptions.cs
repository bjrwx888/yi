using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Sms.Aliyun
{
    public class SmsAliyunOptions
    {
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }

        public string SignName { get; set; }


        public string TemplateCode { get; set; }
        public bool EnableFeature { get; set; } = true;
    }
}
