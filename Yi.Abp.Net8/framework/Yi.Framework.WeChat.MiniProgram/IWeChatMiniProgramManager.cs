using Yi.Framework.WeChat.MiniProgram.HttpModels;

namespace Yi.Framework.WeChat.MiniProgram;

public interface IWeChatMiniProgramManager
{
    /// <summary>
    /// 获取用户openid
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<Code2SessionResponse> Code2SessionAsync(Code2SessionInput input);
}