using Mapster;
using SqlSugar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Market;
using Yi.Framework.DigitalCollectibles.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.DigitalCollectibles.Application.Services;

/// <summary>
/// 市场应用服务
/// </summary>
public class MarketService:ApplicationService
{
    private readonly ISqlSugarRepository<MarketGoodsAggregateRoot> _marketGoodsRepository;

    public MarketService(ISqlSugarRepository<MarketGoodsAggregateRoot> marketGoodsRepository)
    {
        _marketGoodsRepository = marketGoodsRepository;
    }

    /// <summary>
    /// 交易市场查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<MarketGetListOutputDto>> GetListAsync(MarketGetListInput input)
    {
        RefAsync<int> total = 0;
        var entities = await _marketGoodsRepository._DbQueryable.WhereIF(
                input.StartTime is not null && input.EndTime is not null,
                x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
            .OrderByDescending(x => x.CreationTime)
            .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);
        var output = entities.Adapt<List<MarketGetListOutputDto>>();
        return new PagedResultDto<MarketGetListOutputDto>(total, output);
    }
}