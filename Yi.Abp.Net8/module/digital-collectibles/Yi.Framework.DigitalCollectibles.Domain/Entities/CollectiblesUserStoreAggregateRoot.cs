﻿using SqlSugar;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yi.Framework.DigitalCollectibles.Domain.Entities;

/// <summary>
/// 数字藏品用户存储表
/// 表示用户与藏品的库存关系
/// </summary>
[SugarTable("DC_CollectiblesUserStore")]
public class CollectiblesUserStoreAggregateRoot:FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 用户id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 藏品id
    /// </summary>
    public Guid CollectiblesId { get; set; }

    /// <summary>
    /// 用户是否已读
    /// </summary>
    public bool IsRead { get; set; }
}