using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Furion.JsonSerialization;
using Mapster;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Yi.Framework.Module.WeChat.Extensions;
using Yi.Framework.Module.WeChat.Model;
using Yi.Framework.Module.WeChat.Token;

namespace Yi.Framework.Module.WeChat
{
    public class WeChatManager : IWeChatManager, ISingleton
    {
        private WeChatOptions _options;
        private IWeChatToken _weChatToken;
        public WeChatManager(IOptions<WeChatOptions> options, IWeChatToken weChatToken)
        {
            _options = options.Value;
            _weChatToken = weChatToken;
        }

        /// <summary>
        /// 获取用户openid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Code2SessionResponse> Code2SessionAsync(Code2SessionInput input)
        {
            string url = "https://api.weixin.qq.com/sns/jscode2session";
            var req = new Code2SessionRequest();
            req.js_code = input.js_code;
            req.secret = _options.AppSecret;
            req.appid = _options.AppId;

            using (HttpClient httpClient = new HttpClient())
            {
                string queryString = req.ToQueryString();
                var builder = new UriBuilder(url);
                builder.Query = queryString;
                HttpResponseMessage response = await httpClient.GetAsync(builder.ToString());
                var responseBody = await response.Content.ReadFromJsonAsync<Code2SessionResponse>();

                responseBody.ValidateSuccess();

                return responseBody;
            }
        }

        /// <summary>
        /// 支付预支付id，描述必填
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsApiResponse> JsApiAsync(JsApiInput input)
        {
            string url = "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";

            var req = input.Adapt<JsApiRequest>();
            req.mchid = _options.Mchid;
            req.notify_url = _options.NotifyUrl;
            req.appid = _options.AppId;

            using (HttpClient httpClient = new HttpClient(new WeChatPayHttpHandler(_options.Mchid, _options.MerchantSerialNo)))
            {// 设置Accept头
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                // 设置User-Agent头
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36");
                JsonContent jsonContent = JsonContent.Create(req);
                HttpResponseMessage response = await httpClient.PostAsync(url, jsonContent);
                var data = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadFromJsonAsync<JsApiResponse>();
                return responseBody;
            }
        }

        /// <summary>
        /// 支付通知回调
        /// </summary>
        /// <returns></returns>
        public PayNoticeResult PayNotice(PayNoticeReponse reponse)
        {
            var data = reponse.resource;
            var result = AEAD_AES_256_GCM(data.associated_data, data.nonce, data.ciphertext, _options.PaySecretKey);
            var output = JSON.Deserialize<PayNoticeResult>(result);
            return output;
        }


        /// <summary>
        /// 获取不限制的小程序码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<string> GetQRCodeAsync(string scene, string page, EnvVersionEnum unlimitedQRCodeEnum = EnvVersionEnum.release)
        {
            string url = $"https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={await _weChatToken.GetTokenAsync()}";
            var req = new UnlimitedQRCodeRequest(unlimitedQRCodeEnum);
            req.scene = scene;
            req.page = page;
            req.env_version = unlimitedQRCodeEnum.ToString();
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent message = new StringContent(System.Text.Json.JsonSerializer.Serialize(req));

                HttpResponseMessage response = await httpClient.PostAsync(url, message);

                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var result = ConvertStreamToBase64(stream);
                return result;
            }
        }

        /// <summary>
        /// 小程序推送订阅消息
        /// </summary>
        /// <returns></returns>
        public async Task SendUniformMessageAsync(UniformMessageInput input)
        {
            string url = $"https://api.weixin.qq.com/cgi-bin/message/wxopen/template/uniform_send?access_token={await _weChatToken.GetTokenAsync()}";
            var req = input.Adapt<UniformMessageRequest>();
            req.mp_template_msg.template_id = _options.OfficialAccounts.TemplateId;
            req.mp_template_msg.appid = _options.OfficialAccounts.AppId;
            req.mp_template_msg.miniprogram.appid = _options.AppId;
 
            using (HttpClient httpClient = new HttpClient())
            {
                var body =new StringContent(JSON.Serialize(req));
                HttpResponseMessage response = await httpClient.PostAsync(url, body);
                var responseBody = await response.Content.ReadFromJsonAsync<UniformMessageResponse>();

                responseBody.ValidateSuccess();
            }

        }


        /// <summary>
        /// AEAD_AES_256_GCM解密算法，用于解开支付回调的通知
        /// </summary>
        /// <param name="ciphertextBase64">需要base64</param>
        /// <param name="associatedDataBase64">需要base64</param>
        /// <param name="nonceBase64">需要base64</param>
        /// <returns></returns>
        public static string AEAD_AES_256_GCM(string associatedData, string nonce, string ciphertext, string secretKey)
        {
            try
            {
                GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
                AeadParameters aeadParameters = new AeadParameters(
                    new KeyParameter(Encoding.UTF8.GetBytes(secretKey)),
                    128,
                    Encoding.UTF8.GetBytes(nonce),
                    Encoding.UTF8.GetBytes(associatedData));
                gcmBlockCipher.Init(false, aeadParameters);

                byte[] data = Convert.FromBase64String(ciphertext);
                byte[] plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
                int length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
                gcmBlockCipher.DoFinal(plaintext, length);
                return Encoding.UTF8.GetString(plaintext);
            }
            catch (Exception ex)
            {
                throw new WeChatException("支付回调解密错误", ex);
            }
        }

        private string ConvertStreamToBase64(Stream stream)
        {
            // 将Stream对象转换为字节数组
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int)stream.Length);

            // 将字节数组转换为Base64字符串
            string base64String = Convert.ToBase64String(buffer);

            return base64String;
        }



    }
}
