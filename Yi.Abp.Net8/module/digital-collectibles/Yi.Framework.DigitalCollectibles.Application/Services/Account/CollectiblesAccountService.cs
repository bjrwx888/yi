using Volo.Abp.Application.Services;

namespace Yi.Framework.DigitalCollectibles.Application.Services.Account;

public class CollectiblesAccountService: ApplicationService
{
    /// <summary>
    /// 小程序登录
    /// </summary>
    /// <returns></returns>
    public Task PostLoginAsync()
    {
        throw new NotImplementedException();
        //根据code去获取wxid
        //判断wxid中是否有对应的userid关系
        //果然有，直接根据userid返回该用户token
        //如果没有，返回结果即可
    }
    
    
    /// <summary>
    /// 小程序绑定账号
    /// </summary>
    /// <returns></returns>
    public Task PostBindAsync()
    {
        throw new NotImplementedException();
        //根据code去获取wxid
        //校验手机号
        //根据手机号查询用户信息
        //将wxid和用户user绑定
    }
    
    //小程序注册
    public Task PostRegisterAsync()
    {
        throw new NotImplementedException();
        //走普通注册流程
        //同时再加一个小程序绑定即可
    }
}