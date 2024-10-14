using SqlSugar;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yi.Framework.DigitalCollectibles.Domain.Entities;

/// <summary>
/// 数字藏品用户存储表
/// 表示用户与藏品的库存关系
/// </summary>
[SugarTable("DC_CollectiblesUserStore")]
public class CollectiblesUserStoreAggregateRoot:FullAuditedAggregateRoot<Guid>
{
    
}