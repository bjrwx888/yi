using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Microsoft.Extensions.Options;
using Yi.Framework.Module.WeChat;
using Yi.Framework.Module.WeChat.Extensions;
using Yi.Framework.Module.WeChat.Model;
using Yi.Framework.Module.WeChat.Token;

namespace Zeng.CarMovingAssistant.Application.CarMoving.Services.Impl
{
    public class DefaultWeChatToken : IWeChatToken,ITransient
    {
        private WeChatOptions _options;
        public DefaultWeChatToken(IOptions<WeChatOptions> options)
        {
            _options = options.Value;
        }
        public async Task<string> GetTokenAsync()
        {
            var token = await this.GetAccessToken();
            return token.access_token;
        }

        /// <summary>
        /// 获取微信AccessToken
        /// </summary>
        public async Task<AccessTokenResponse> GetAccessToken()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/token";
            var req = new AccessTokenRequest();
            req.appid = _options.AppId;
            req.secret = _options.AppSecret;
            req.grant_type = "client_credential";
            using (HttpClient httpClient = new HttpClient())
            {
                string queryString = req.ToQueryString();
                var builder = new UriBuilder(url);
                builder.Query = queryString;
                HttpResponseMessage response = await httpClient.GetAsync(builder.ToString());

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
                return responseBody;
            }
        }
    }
}
