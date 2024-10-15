using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;
using Yi.Framework.DigitalCollectibles.Domain.Managers;

namespace Yi.Framework.DigitalCollectibles.Application.Jobs;

/// <summary>
/// 自动刷新填满矿池
/// </summary>
public class AutoRefreshMiningPoolJob : QuartzBackgroundWorkerBase
{
    private readonly MiningPoolManager _miningPoolManager;

    public AutoRefreshMiningPoolJob()
    {
        JobDetail = JobBuilder.Create<AutoRefreshMiningPoolJob>().WithIdentity(nameof(AutoRefreshMiningPoolJob))
            .Build();

        //每天早上10点执行一次
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(AutoRefreshMiningPoolJob))
            .WithCronSchedule("0 0 10 * * ?")
            .Build();
    }

    public override async Task Execute(IJobExecutionContext context)
    {
        await _miningPoolManager.RefreshMiningPoolAsync();
    }
}