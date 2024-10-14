using SqlSugar;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yi.Framework.DigitalCollectibles.Domain.Entities;

/// <summary>
/// 挂机表
/// 表示用户与挂机道具之间的关系
/// 用于定时任务处理自动挖矿
/// </summary>
[SugarTable("DC_OnHook")]
public class OnHookAggregateRoot:FullAuditedAggregateRoot<Guid>
{
    
}