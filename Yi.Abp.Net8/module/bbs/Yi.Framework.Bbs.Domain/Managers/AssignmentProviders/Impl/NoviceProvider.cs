using Yi.Framework.Bbs.Domain.Entities.Assignment;

namespace Yi.Framework.Bbs.Domain.Managers.AssignmentProviders;

/// <summary>
///     新手任务提供者
/// </summary>
public class NoviceProvider : IAssignmentProvider
{
    public Task<List<AssignmentDefineAggregateRoot>> GetCanReceiveListAsync(AssignmentContext context)
    {
        throw new NotImplementedException();
    }

    public Task VerifyCanAcceptAsync(AssignmentContext context)
    {
        throw new NotImplementedException();
    }
}