using SqlSugar;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yi.Framework.DigitalCollectibles.Domain.Entities;

/// <summary>
/// 数字藏品定义表
/// 用于定义数字藏品
/// </summary>
[SugarTable("DC_Collectibles")]
public class CollectiblesAggregateRoot:FullAuditedAggregateRoot<Guid>
{
    
}