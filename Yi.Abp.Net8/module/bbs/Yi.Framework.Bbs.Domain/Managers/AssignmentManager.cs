using Volo.Abp.Domain.Services;
using Yi.Framework.Bbs.Domain.Entities.Assignment;

namespace Yi.Framework.Bbs.Domain.Managers;

/// <summary>
/// 任务领域，任务相关核心逻辑
/// </summary>
public class AssignmentManager : DomainService
{
    /// <summary>
    /// 接受任务
    /// </summary>
    /// <param name="userId">领取用户</param>
    /// <param name="asignmentDefineId">任务定义id</param>
    /// <returns></returns>
    public Task AcceptAsync(Guid userId, Guid asignmentDefineId)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// 领取任务奖励
    /// </summary>
    /// <param name="asignmentId">任务id</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task ReceiveRewardsAsync(Guid asignmentId)
    {
        throw new NotImplementedException();
    }
    
    
    /// <summary>
    /// 根据用户id获取能够领取的任务列表
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <returns></returns>
    public Task<List<AssignmentDefineAggregateRoot>> GetCanReceiveListAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}