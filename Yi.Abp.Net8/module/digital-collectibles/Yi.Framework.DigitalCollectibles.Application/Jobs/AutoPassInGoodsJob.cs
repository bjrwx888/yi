using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;
using Yi.Framework.DigitalCollectibles.Domain.Managers;

namespace Yi.Framework.DigitalCollectibles.Application.Jobs;
/// <summary>
/// 自动下架商品
/// </summary>
public class AutoPassInGoodsJob: QuartzBackgroundWorkerBase
{
    private readonly MarketManager _marketManager;
    private readonly ILogger<AutoPassInGoodsJob> _logger;
    public AutoPassInGoodsJob(MarketManager marketManager, ILogger<AutoPassInGoodsJob> logger)
    {
        _marketManager = marketManager;
        _logger = logger;
        JobDetail = JobBuilder.Create<AutoPassInGoodsJob>().WithIdentity(nameof(AutoPassInGoodsJob))
            .Build();

        //每小时,第10分钟执行一次
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(AutoPassInGoodsJob))
            .WithCronSchedule("0 10 * * * ?")
            .Build();
    }


    public override async Task Execute(IJobExecutionContext context)
    {
      await  _marketManager.AutoPassInGoodsAsync();
    }
}