using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Hangfire;

namespace Yi.Framework.BackgroundWorkers.Hangfire;

[DependsOn(typeof(AbpBackgroundWorkersHangfireModule))]
public class YiFrameworkBackgroundWorkersHangfireModule:AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new YiHangfireConventionalRegistrar());
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        //定时任务自动注入，Abp默认只有在Quartz才实现
        var backgroundWorkerManager = context.ServiceProvider.GetRequiredService<IBackgroundWorkerManager>();
        var works = context.ServiceProvider.GetServices<IHangfireBackgroundWorker>();
            
        foreach (var work in works)
        {
            await backgroundWorkerManager.AddAsync(work);
        }

    }
}