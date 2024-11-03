using Volo.Abp.Domain.Services;
using Volo.Abp.Users;
using Yi.Framework.DigitalCollectibles.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.DigitalCollectibles.Domain.Managers;
/// <summary>
/// 藏品领域服务
/// 用于管理用户的藏品库存、藏品的业务逻辑
/// </summary>
public class CollectiblesManager:DomainService
{
    private readonly ISqlSugarRepository<CollectiblesUserStoreAggregateRoot> _collectiblesUserStoreRepository;

    public CollectiblesManager(ISqlSugarRepository<CollectiblesUserStoreAggregateRoot> collectiblesUserStoreRepository)
    {
        _collectiblesUserStoreRepository = collectiblesUserStoreRepository;
    }


    public async Task<decimal> GetAccountValueAsync(Guid userId)
    {
        var collectiblesList = await _collectiblesUserStoreRepository._DbQueryable
            .Where(store => store.UserId == userId)
            .LeftJoin<CollectiblesAggregateRoot>((store, c) => store.CollectiblesId == c.Id)
            .Select((store, c) =>
                new
                {
                    c.Id,
                    c.ValueNumber
                }
            ).ToListAsync();
        var groupBy = collectiblesList.GroupBy(x => x.Id);
        decimal totalValue = 0;

        //首个价值百分之百，后续每个只有百分之40，最大10个
        foreach (var groupByItem in groupBy)
        {
            foreach (var item in groupByItem.Select((value, index) => new { value, index }))
            {
                
                if (item.index == 0)
                {
                    totalValue += item.value.ValueNumber;
                }
                else if (item.index == 10)
                {
                    //到第11个，直接跳出循环
                    break;
                }
                else
                {
                    totalValue += item.value.ValueNumber * 0.4m;
                }
            }
        }

        return totalValue;
    }
}