using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Module.WeChat.Model;

namespace Yi.Framework.Module.WeChat
{
    public interface IWeChatManager
    {
        /// <summary>
        /// 获取用户openid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Code2SessionResponse> Code2SessionAsync(Code2SessionInput input);

        /// <summary>
        /// 获取不限制的小程序码
        /// <param name="scene"></param>
        /// <returns></returns>
        /// </summary>
        Task<string> GetQRCodeAsync(string scene, string page, EnvVersionEnum unlimitedQRCodeEnum = EnvVersionEnum.release);

        /// <summary>
        /// 支付预支付id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsApiResponse> JsApiAsync(JsApiInput input);

        /// <summary>
        /// 支付的回调接口
        /// </summary>
        /// <param name="reponse"></param>
        /// <returns></returns>
        PayNoticeResult PayNotice(PayNoticeReponse reponse);

        /// <summary>
        /// 发送聚合消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SendUniformMessageAsync(UniformMessageInput input);
    }
}
