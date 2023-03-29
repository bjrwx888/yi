using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.SDK.Dysmsapi20170525;
using Tea;

namespace Yi.Framework.Sms.Aliyun
{
    public class SmsAliyunManager
    {
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

        public Client  AliyunClient { get; set; }
        public SmsAliyunManager() {

            AliyunClient = CreateClient("accessKeyId", "accessKeySecret");
        }

        public async Task Send(string phoneNumbers, string code)
        {
            try
            {
                AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest sendSmsRequest = new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
                {
                    PhoneNumbers = phoneNumbers,
                    SignName = "",
                    TemplateCode = code,
                };

             var response= await  AliyunClient.SendSmsAsync(sendSmsRequest);
            }
            catch (TeaException error)
            {

                Console.WriteLine(error.Message); 
            }
            catch (Exception _error)
            {
                Console.WriteLine(_error.Message);
            }
        }
    } 
}