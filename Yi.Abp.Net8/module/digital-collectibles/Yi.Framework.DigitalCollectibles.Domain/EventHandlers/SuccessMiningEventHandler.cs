using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Yi.Framework.DigitalCollectibles.Domain.Entities;
using Yi.Framework.DigitalCollectibles.Domain.Entities.Record;
using Yi.Framework.DigitalCollectibles.Domain.Managers;

using Yi.Framework.DigitalCollectibles.Domain.Shared.Etos;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.DigitalCollectibles.Domain.EventHandlers;

/// <summary>
/// 成功挖到矿物
/// </summary>
public class SuccessMiningEventHandler : ILocalEventHandler<SuccessMiningEto>, ITransientDependency
{
    private MiningPoolManager _miningPoolManager;
    private ISqlSugarRepository<CollectiblesAggregateRoot> _repository;
    private readonly ISqlSugarRepository<CollectiblesUserStoreAggregateRoot> _userStoreRepository;
    private readonly ISqlSugarRepository<MiningPoolRecordAggregateRoot> _miningPoolRecordRepository;
    public SuccessMiningEventHandler(MiningPoolManager miningPoolManager,
        ISqlSugarRepository<CollectiblesAggregateRoot> repository, ISqlSugarRepository<CollectiblesUserStoreAggregateRoot> userStoreRepository, ISqlSugarRepository<MiningPoolRecordAggregateRoot> miningPoolRecordRepository)
    {
        _miningPoolManager = miningPoolManager;
        _repository = repository;
        _userStoreRepository = userStoreRepository;
        _miningPoolRecordRepository = miningPoolRecordRepository;
    }

    public async Task HandleEventAsync(SuccessMiningEto eventData)
    {
        //当前藏品
        var currentCollectibles = await _repository.GetFirstAsync(x => x.Id == eventData.CollectiblesId);
        //扣减矿池
        await _miningPoolManager.DeductionPoolAsync(currentCollectibles.Rarity);
        //新增全世界发现
        currentCollectibles.FindTotal += 1;
        await _repository.UpdateAsync(currentCollectibles);
        
        //使用结果新增给对应的用户
        await _userStoreRepository.InsertAsync(new CollectiblesUserStoreAggregateRoot
        {
            UserId = eventData.UserId,
            CollectiblesId = eventData.CollectiblesId,
            IsRead = false
        });
        
        //新增一条挖矿记录
        await _miningPoolRecordRepository.InsertAsync(new MiningPoolRecordAggregateRoot(eventData.UserId,eventData.CollectiblesId));
    }
}