using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Yi.Framework.Core.Extensions;
using Yi.Framework.WeChat.MiniProgram.HttpModels;
using Yi.Framework.WeChat.MiniProgram.Token;

namespace Yi.Framework.WeChat.MiniProgram;

public class WeChatMiniProgramManager : IWeChatMiniProgramManager, ISingletonDependency
{
    private IMiniProgramToken _weChatToken;
    private WeChatMiniProgramOptions _options;

    public WeChatMiniProgramManager(IMiniProgramToken weChatToken, IOptions<WeChatMiniProgramOptions> options)
    {
        _weChatToken = weChatToken;
        _options = options.Value;
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
        req.appid = _options.AppID;

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
}