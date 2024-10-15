using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Collectibles;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Market;
using Yi.Framework.DigitalCollectibles.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.DigitalCollectibles.Application.Services;

/// <summary>
/// 藏品应用服务
/// </summary>
public class CollectiblesService : ApplicationService
{
    private readonly ISqlSugarRepository<CollectiblesUserStoreAggregateRoot> _collectiblesUserStoreRepository;

    public CollectiblesService(ISqlSugarRepository<CollectiblesUserStoreAggregateRoot> collectiblesUserStoreRepository)
    {
        _collectiblesUserStoreRepository = collectiblesUserStoreRepository;
    }

    /// <summary>
    /// 获取当前用户的藏品
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("collectibles/user")]
    [Authorize]
    public async Task<PagedResultDto<CollectiblesUserGetOutputDto>> GetForAccountUserAsync(
        CollectiblesUserGetInput input)
    {
        RefAsync<int> total = 0;
        var entities = await _collectiblesUserStoreRepository._DbQueryable.WhereIF(
                input.StartTime is not null && input.EndTime is not null,
                x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
            .OrderByDescending(x => x.CreationTime)
            .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);
        var output = entities.Adapt<List<CollectiblesUserGetOutputDto>>();
        return new PagedResultDto<CollectiblesUserGetOutputDto>(total, output);
    }
}