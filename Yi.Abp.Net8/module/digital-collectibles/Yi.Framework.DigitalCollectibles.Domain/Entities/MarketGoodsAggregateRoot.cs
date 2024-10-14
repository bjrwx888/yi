using SqlSugar;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yi.Framework.DigitalCollectibles.Domain.Entities;

/// <summary>
/// 交易市场货物
/// 用于表示交易市场货物的情况
/// </summary>
[SugarTable("DC_MarketGoods")]
public class MarketGoodsAggregateRoot:FullAuditedAggregateRoot<Guid>
{
    
}