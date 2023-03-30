using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.SDK.Dysmsapi20170525;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tea;

namespace Yi.Framework.Sms.Aliyun
{
    public class SmsAliyunManager
    {
        public Client AliyunClient { get; set; }
        private ILogger<SmsAliyunManager> _logger;
        private SmsAliyunOptions Options { get; set; }
        public SmsAliyunManager(ILogger<SmsAliyunManager> logger, IOptions<SmsAliyunOptions> options)
        {
            Options = options.Value;
            if (Options.EnableFeature)
            {
                _logger = logger;
                AliyunClient = CreateClient(Options.AccessKeyId, Options.AccessKeySecret);
            }

        }

        private static Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 必填，您的 AccessKey ID
                AccessKeyId = accessKeyId,
                // 必填，您的 AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "dysmsapi.aliyuncs.com";
            return new Client(config);
        }


        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phoneNumbers"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task Send(string phoneNumbers, string code)
        {
            try
            {
                AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest sendSmsRequest = new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
                {
                    PhoneNumbers = phoneNumbers,
                    SignName = Options.SignName,
                    TemplateCode = Options.TemplateCode,
                    TemplateParam = System.Text.Json.JsonSerializer.Serialize(new {  code })
                };

                var response = await AliyunClient.SendSmsAsync(sendSmsRequest);
            }

            catch (Exception _error)
            {
                _logger.LogError(_error, _error.Message);
            }
        }
    }
}