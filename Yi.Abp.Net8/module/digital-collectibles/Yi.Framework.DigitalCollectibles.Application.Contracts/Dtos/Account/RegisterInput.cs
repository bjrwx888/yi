﻿namespace Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Account;

public class RegisterInput
{
    
    //电话号码，根据code的表示来获取

    /// <summary>
    /// 账号
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 唯一标识码
    /// </summary>
    public string? Uuid { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public long Phone { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    public string? Code { get; set; }
        
    /// <summary>
    /// 昵称
    /// </summary>
    public string? Nick{ get; set; }


    /// <summary>
    /// 微信小程序code
    /// </summary>
    public string JsCode { get; set; }
}