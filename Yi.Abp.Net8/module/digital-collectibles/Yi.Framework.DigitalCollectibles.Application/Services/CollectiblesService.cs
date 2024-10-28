﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Collectibles;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Market;
using Yi.Framework.DigitalCollectibles.Domain.Entities;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Consts;
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
    /// 获取该用户的信息
    /// </summary>
    [HttpGet("collectibles/account")]
    [Authorize]
    public async Task<CollectiblesAccountInfoDto> GetAccountInfoAsync()
    {
        var userId = CurrentUser.GetId();
        var totalValue = await _collectiblesUserStoreRepository._DbQueryable.Where(store => store.UserId == userId)
            .LeftJoin<CollectiblesAggregateRoot>((store, c) => store.CollectiblesId == c.Id)
            .SumAsync((store, c) => c.ValueNumber);
        return new CollectiblesAccountInfoDto
        {
            TotalValue = totalValue
        };
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
        var userId = CurrentUser.GetId();
        RefAsync<int> total = 0;
        var output = await _collectiblesUserStoreRepository._DbQueryable
            .Where(x=>x.UserId==userId)
            .WhereIF(
                input.StartTime is not null && input.EndTime is not null,
                u => u.CreationTime >= input.StartTime && u.CreationTime <= input.EndTime)
            .LeftJoin<CollectiblesAggregateRoot>((u, c) => u.CollectiblesId == c.Id)
            .OrderBy((u, c) => c.OrderNum)
            .GroupBy((u, c) => u.CollectiblesId)
            .Select((u, c) =>
                new CollectiblesUserGetOutputDto
                {
                    Id = c.Id,
                    Collectibles = new CollectiblesDto
                    {
                        Id = c.Id,
                        Code = c.Code,
                        Name = c.Name,
                        Describe = c.Describe,
                        ValueNumber = c.ValueNumber,
                        Url = c.Url,
                        Rarity = c.Rarity,
                        FindTotal = c.FindTotal,
                        OrderNum = c.OrderNum
                    },
                    Number = SqlFunc.AggregateCount(u.CollectiblesId)
                })
            .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);
        return new PagedResultDto<CollectiblesUserGetOutputDto>(total, output);
    }
}