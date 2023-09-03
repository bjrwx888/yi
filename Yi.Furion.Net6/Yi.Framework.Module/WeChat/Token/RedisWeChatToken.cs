using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Microsoft.Extensions.Options;
using Yi.Framework.Module.Caching;
using Yi.Framework.Module.WeChat.Extensions;
using Yi.Framework.Module.WeChat.Model;

namespace Yi.Framework.Module.WeChat.Token
{
    public class RedisWeChatToken : IWeChatToken
    {
        private WeChatOptions _options;
        private RedisCacheClient _cacheManager;
        private const string RedisAccessTokenKey = $"WeChat:AccessTokenKey";

        public RedisWeChatToken(IOptions<WeChatOptions> options, RedisCacheClient cacheManager)
        {
            _options = options.Value;
            _cacheManager = cacheManager;
        }
        public async Task<string> GetTokenAsync()
        {
            string accessToken;

            if (_cacheManager.Client.Exists(RedisAccessTokenKey))
            {
                accessToken = _cacheManager.Client.Get(RedisAccessTokenKey);
            }
            else
            {
                var response = await this.GetAccessToken();
                accessToken = response.access_token;
                _cacheManager.Client.Set(RedisAccessTokenKey, accessToken, TimeSpan.FromHours(1));
            }

            return accessToken;
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
                if (string.IsNullOrEmpty(responseBody?.access_token))
                {
                    throw new WeChatException($"获取accessToken异常，返回结果【{await response.Content.ReadAsStringAsync()}】");
                }
                return responseBody;
            }
        }
    }
}
