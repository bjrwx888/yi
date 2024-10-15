﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.MiningPool;
using Yi.Framework.DigitalCollectibles.Domain.Managers;

namespace Yi.Framework.DigitalCollectibles.Application.Services;

public class MiningPoolService : ApplicationService
{
    private readonly MiningPoolManager _manager;

    /// <summary>
    /// 获取矿池状态
    /// </summary>
    /// <returns></returns>
    [HttpGet("mining-pool")]
    public async Task<MiningPoolGetOutput> GetMiningPoolContentAsync()
    {
        var content = await _manager.GetMiningPoolContentAsync();
        var output = content.Adapt<MiningPoolGetOutput>();
        return output;
    }


    /// <summary>
    /// 用户手动挖矿
    /// </summary>
    /// <returns></returns>
    [HttpPost("mining-pool/mining")]
    [Authorize]
    public async Task<MiningResultOutput> MiningAsync()
    {
        var userId = CurrentUser.GetId();
        var result = await _manager.MiningAsync(userId);
        var output = result.Adapt<MiningResultOutput>();
        return output;
    }
}