using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Consts;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Etos;
using Yi.Framework.Rbac.Application.Contracts.IServices;
using Yi.Framework.WeChat.MiniProgram;
using Yi.Framework.WeChat.MiniProgram.HttpModels;

namespace Yi.Framework.DigitalCollectibles.Domain.Managers;

public class WeChatMiniProgramNoticeEventHandler : ILocalEventHandler<WeChatMiniProgramNoticeEto>, ITransientDependency
{
    private readonly IWeChatMiniProgramManager _weChatMiniProgramManager;
    private readonly IAuthService _authService;

    public WeChatMiniProgramNoticeEventHandler(IWeChatMiniProgramManager weChatMiniProgramManager,
        IAuthService authService)
    {
        _weChatMiniProgramManager = weChatMiniProgramManager;
        _authService = authService;
    }

    public async Task HandleEventAsync(WeChatMiniProgramNoticeEto eventData)
    {
        var authInfo = await _authService.TryGetAuthInfoAsync(null, AuthTypeConst.WeChatMiniProgram, eventData.UserId);
        await SendAsync(authInfo.OpenId, eventData.Title);
    }

    /// <summary>
    /// 像用户发送微信消息
    /// </summary>
    /// <param name="userId"></param>
    public async Task SendAsync(string openId, string title)
    {
        //成功挖到矿，可以发消息给用户了
        await _weChatMiniProgramManager.SendSubscribeNoticeAsync(new SubscribeNoticeInput
        {
            touser = openId,
            data = new Dictionary<string, keyValueItem>()
            {
                //活动名称
                { "thing9", new keyValueItem("恭喜挖到新的数字藏品") },

                //奖品名称
                { "thing1", new keyValueItem(title) },

                //中奖时间
                { "date5", new keyValueItem(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")) },

                //温馨提醒
                { "thing4", new keyValueItem("点击前往小程序，可在仓库或者记录中查看") },
            }
        });
    }
}