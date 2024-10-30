using Microsoft.AspNetCore.Authorization;
using SqlSugar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;
using Yi.Framework.Ddd.Application.Contracts;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Collectibles;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Records;
using Yi.Framework.DigitalCollectibles.Domain.Entities;
using Yi.Framework.DigitalCollectibles.Domain.Entities.Record;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.DigitalCollectibles.Application.Services.Record;

public class CollectiblesServiceRecordService : ApplicationService
{
    private readonly ISqlSugarRepository<MiningPoolRecordAggregateRoot> _miningPoolRecordRepository;
    private readonly ISqlSugarRepository<MarketRecordAggregateRoot> _marketRecordRepository;
    public CollectiblesServiceRecordService(ISqlSugarRepository<MiningPoolRecordAggregateRoot> miningPoolRecordRepository, ISqlSugarRepository<MarketRecordAggregateRoot> marketRecordRepository)
    {
        _miningPoolRecordRepository = miningPoolRecordRepository;
        _marketRecordRepository = marketRecordRepository;
    }

    /// <summary>
    /// 获取当前用户的挖矿记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<PagedResultDto<MiningPoolRecordDto>> GetMiningPoolAsync(PagedAllResultRequestDto input)
    {
        RefAsync<int> total = 0;
        var userId = CurrentUser.GetId();
        var output =  await _miningPoolRecordRepository._DbQueryable.WhereIF(input.StartTime is not null && input.EndTime is not null,
                x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreationTime)
            .LeftJoin<CollectiblesAggregateRoot>((x, c) => x.CollectiblesId==c.Id)
            .Select((x, c) =>
                new MiningPoolRecordDto
                {
                    Id = x.Id,
                    Collectibles = new CollectiblesDto()
                    {
                        Id = c.Id
                    }
                },true
            )
            .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);

        return new PagedResultDto<MiningPoolRecordDto>(total,output);
    }

    /// <summary>
    /// 获取当前用户的交易记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<PagedResultDto<MarketRecordDto>> GetMarketAsync(PagedAllResultRequestDto input)
    {
        RefAsync<int> total = 0;
        var userId = CurrentUser.GetId();
        var output =  await _marketRecordRepository._DbQueryable.WhereIF(input.StartTime is not null && input.EndTime is not null,
                x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
            .Where(x => x.SellUserId == userId)
            .OrderByDescending(x => x.CreationTime)
            .LeftJoin<CollectiblesAggregateRoot>((x, c) => x.CollectiblesId==c.Id)
            .Select((x, c) =>
                    new MarketRecordDto
                    {
                        Id = x.Id,
                        Collectibles = new CollectiblesDto()
                        {
                            Id = c.Id
                        }
                    },true
            )
            .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);
        
        return new PagedResultDto<MarketRecordDto>(total,output);
    }
}