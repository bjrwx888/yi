namespace Yi.Framework.Bbs.Domain.Managers.AssignmentProviders;

/// <summary>
///     每周任务提供者
/// </summary>
public class WeeklyProvider : TimerProvider
{
    protected override TimeSpan TimeCycle => TimeSpan.FromDays(7);
}