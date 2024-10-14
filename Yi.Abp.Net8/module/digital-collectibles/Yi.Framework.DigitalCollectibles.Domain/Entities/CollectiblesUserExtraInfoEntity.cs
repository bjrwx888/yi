using SqlSugar;
using Volo.Abp.Domain.Entities;

namespace Yi.Framework.DigitalCollectibles.Domain.Entities;


/// <summary>
/// 藏品用户信息表
/// </summary>
[SugarTable("DC_CollectiblesUserExtraInfo")]
public class CollectiblesUserExtraInfoEntity: Entity<Guid>
{
    /// <summary>
    /// 用户id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone{ get; set; }
    
    /// <summary>
    /// 微信openid
    /// </summary>
    public string WeChatOpenId { get; set; }
}