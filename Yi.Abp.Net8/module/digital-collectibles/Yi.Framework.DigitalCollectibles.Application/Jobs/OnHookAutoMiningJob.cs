using Microsoft.Extensions.Logging;
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
    private readonly ILogger<OnHookAutoMiningJob> _logger;

    public OnHookAutoMiningJob(MiningPoolManager miningPoolManager, ILogger<OnHookAutoMiningJob> logger)
    {
        _miningPoolManager = miningPoolManager;
        _logger = logger;
        JobDetail = JobBuilder.Create<OnHookAutoMiningJob>().WithIdentity(nameof(OnHookAutoMiningJob))
            .Build();

        //每小时执行一次
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(OnHookAutoMiningJob))
            // .WithCronSchedule("10 * * * * ?")
            .WithCronSchedule("0 0 * * * ?")
            .Build();
    }

    public override async Task Execute(IJobExecutionContext context)
    {
        await _miningPoolManager.OnHookMiningAsync();
    }
}