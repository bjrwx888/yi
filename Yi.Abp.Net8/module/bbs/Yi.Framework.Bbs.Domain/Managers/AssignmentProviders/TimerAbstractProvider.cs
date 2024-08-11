using Yi.Framework.Bbs.Domain.Entities.Assignment;

namespace Yi.Framework.Bbs.Domain.Managers.AssignmentProviders;

/// <summary>
/// 定时任务提供者
/// </summary>
public abstract class TimerProvider : IAssignmentProvider
{
    /// <summary>
    /// 时间周期
    /// </summary>
    protected abstract TimeSpan TimeCycle { get; }

    public Task<List<AssignmentDefineAggregateRoot>> GetCanReceiveListAsync(AssignmentContext context)
    {
        throw new NotImplementedException();
    }

    public Task VerifyCanAcceptAsync(AssignmentContext context)
    {
        throw new NotImplementedException();
    }
}