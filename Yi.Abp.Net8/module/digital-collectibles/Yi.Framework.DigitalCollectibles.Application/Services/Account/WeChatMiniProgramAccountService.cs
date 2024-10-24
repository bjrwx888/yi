using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Account;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Consts;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Enums;
using Yi.Framework.Rbac.Application.Contracts.Dtos.Account;
using Yi.Framework.Rbac.Application.Contracts.IServices;
using Yi.Framework.Rbac.Domain.Shared.Enums;
using Yi.Framework.WeChat.MiniProgram;
using Yi.Framework.WeChat.MiniProgram.HttpModels;

namespace Yi.Framework.DigitalCollectibles.Application.Services.Account;

/// <summary>
/// 微信小程序账户应用服务
/// </summary>
public class WeChatMiniProgramAccountService : ApplicationService
{
    private readonly IWeChatMiniProgramManager _weChatMiniProgramManager;
    private readonly IAuthService _authService;
    private readonly IAccountService _accountService;

    public WeChatMiniProgramAccountService(IWeChatMiniProgramManager weChatMiniProgramManager, IAuthService authService,
        IAccountService accountService)
    {
        _weChatMiniProgramManager = weChatMiniProgramManager;
        _authService = authService;
        _accountService = accountService;
    }

    /// <summary>
    /// 使用小程序jsCode登录意社区账号
    /// </summary>
    /// <returns></returns>
    [HttpPost("wechat/mini-program/account/login")]
    public async Task<LoginOutput> PostLoginAsync(LoginInput intput)
    {
        var output = new LoginOutput();
        //根据code去获取wxid
        //判断wxid中是否有对应的userid关系
        //果然有，直接根据userid返回该用户token
        //如果没有，返回结果即可
        var openId = (await _weChatMiniProgramManager.Code2SessionAsync(new Code2SessionInput(intput.JsCode)))
            .openid;

        var authInfo = await _authService.TryGetByOpenIdAsync(openId, AuthTypeConst.WeChatMiniProgram);
        if (authInfo is null)
        {
            throw new UserFriendlyException("该小程序没有绑定任何账号", "2000", "Auth未找到对应关系");
        }

        //根据用户id获取到用户信息
        var result = await _accountService.PostLoginAsync(authInfo.UserId);
        output.Token = result.Token;

        return output;
    }


    /// <summary>
    /// 将小程序第三方授权绑定给意社区账号
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="UserFriendlyException"></exception>
    [HttpPost("wechat/mini-program/account/bind")]
    public async Task PostBindAsync(BindInput input)
    {
        //验证手机号
        await _accountService.ValidationPhoneCaptchaAsync(ValidationPhoneTypeEnum.Bind, input.Phone, input.Code);
        //校验手机号与验证码
        //根据手机号查询用户信息
        //根据code去获取wxid
        //将wxid和用户user绑定
        var userInfo = await _accountService.GetAsync(null, input.Phone);
        if (userInfo is null)
        {
            throw new UserFriendlyException("该手机号未被注册，无法绑定微信小程序");
        }

        //验证手机号的验证码
        var openId = (await _weChatMiniProgramManager.Code2SessionAsync(new Code2SessionInput(input.JsCode))).openid;
        

        await PostBindToAuthAsync(userInfo.User.Id, openId, userInfo.User.UserName);
    }

    private async Task PostBindToAuthAsync(Guid userId, string openId, string? name = null)
    {
        await _authService.CreateAsync(new AuthCreateOrUpdateInputDto
        {
            UserId = userId,
            OpenId = openId,
            Name = name ?? "未知",
            AuthType = AuthTypeConst.WeChatMiniProgram
        });
    }

    /// <summary>
    /// 使用小程序去注册意社区账号
    /// </summary>
    /// <param name="input"></param>
    [HttpPost("wechat/mini-program/account/register")]
    public async Task PostRegisterAsync(RegisterInput input)
    {
        //先校验code，openId
        var openId = (await _weChatMiniProgramManager.Code2SessionAsync(new Code2SessionInput(input.JsCode))).openid;
        
        //走普通注册流程
        //同时再加一个小程序绑定即可
        var userName = GenerateRandomString(6);
        await _accountService.PostTempRegisterAsync(new RegisterDto
        {
            UserName =$"ls-{userName}",
            Password = GenerateRandomString(20),
            Nick = $"临时账号-{userName}"
        });
        var userInfo = await _accountService.GetAsync(userName, null);

        await PostBindToAuthAsync(userInfo.User.Id, openId, userInfo.User.UserName);
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}