using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Market;

namespace Yi.Framework.DigitalCollectibles.Application.Services;

/// <summary>
/// 市场应用服务
/// </summary>
public class MarketService:ApplicationService
{
    /// <summary>
    /// 交易市场查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<PagedResultDto<MarketGetListOutputDto>> GetListAsync(MarketGetListInput input)
    {
        throw new NotImplementedException();
    }
}