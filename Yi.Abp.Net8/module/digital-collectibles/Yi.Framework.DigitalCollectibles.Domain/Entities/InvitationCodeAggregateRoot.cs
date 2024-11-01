using SqlSugar;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yi.Framework.DigitalCollectibles.Domain.Entities;

[SugarTable("DC_InvitationCode")]
public class InvitationCodeAggregateRoot:FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 谁的邀请码
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 是否填写了邀请码（是否被邀请）
    /// </summary>
    public bool IsInvited { get; set; }


    /// <summary>
    /// 积分-邀请数量
    /// </summary>
    public int PointsNumber { get; set; }


    //不做记录
    // /// <summary>
    // /// 邀请者的id
    // /// </summary>
    // public Guid InviterUserId{ get; set; }
}