using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;
using Yi.Framework.DigitalCollectibles.Domain.Entities;
using Yi.Framework.DigitalCollectibles.Domain.Managers;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.DigitalCollectibles.Application.Jobs;

/// <summary>
/// 处理挂机挖矿定时任务
/// </summary>
public class OnHookAutoMiningJob : QuartzBackgroundWorkerBase
{
    private readonly MiningPoolManager _miningPoolManager;

    public OnHookAutoMiningJob(MiningPoolManager miningPoolManager)
    {
        _miningPoolManager = miningPoolManager;
        JobDetail = JobBuilder.Create<AutoRefreshMiningPoolJob>().WithIdentity(nameof(AutoRefreshMiningPoolJob))
            .Build();

        //每小时执行一次
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(AutoRefreshMiningPoolJob))
            .WithCronSchedule("0 0 * * * ?")
            .Build();
    }

    public override async Task Execute(IJobExecutionContext context)
    {
        await _miningPoolManager.OnHookMiningAsync();
    }
}