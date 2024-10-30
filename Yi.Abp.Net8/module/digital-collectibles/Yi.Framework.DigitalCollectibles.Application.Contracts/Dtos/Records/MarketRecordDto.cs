using Volo.Abp.Application.Dtos;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Collectibles;

namespace Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Records;

public class MarketRecordDto:EntityDto<Guid>
{
    public  DateTime CreationTime { get; set; }
    /// <summary>
    /// 出售数量
    /// </summary>
    public int SellNumber{ get; set; }
    
    /// <summary>
    /// 出售者用户id
    /// </summary>
    public Guid SellUserId { get; set; }

    /// <summary>
    /// 购买者用户id
    /// </summary>
    public Guid BuyId { get; set; }
    
    /// <summary>
    /// 出售单价
    /// </summary>
    public decimal UnitPrice{ get; set; }
    
    /// <summary>
    /// 实际到手价格（扣除税收）
    /// </summary>
    public decimal RealTotalPrice{ get; set; }
    /// <summary>
    /// 交易的藏品
    /// </summary>
    public CollectiblesDto Collectibles { get; set; }
}