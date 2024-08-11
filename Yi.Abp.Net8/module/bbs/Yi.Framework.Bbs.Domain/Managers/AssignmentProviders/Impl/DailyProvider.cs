namespace Yi.Framework.Bbs.Domain.Managers.AssignmentProviders;

/// <summary>
///     每日任务提供者
/// </summary>
public class DailyProvider : TimerProvider
{
    protected override TimeSpan TimeCycle => TimeSpan.FromDays(1);
}