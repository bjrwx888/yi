using SqlSugar;
using Volo.Abp.DependencyInjection;
using Yi.Framework.Bbs.Domain.Entities.Assignment;
using Yi.Framework.Bbs.Domain.Shared.Enums;

namespace Yi.Framework.Bbs.Domain.Managers.AssignmentProviders;

/// <summary>
///     新手任务提供者
/// </summary>
[ExposeServices(typeof(IAssignmentProvider))]
public class NoviceProvider : IAssignmentProvider
{
    public async Task<List<AssignmentDefineAggregateRoot>> GetCanReceiveListAsync(AssignmentContext context)
    {
        List<AssignmentDefineAggregateRoot> output = new List<AssignmentDefineAggregateRoot>();
        //新手任务是要有前置依赖关系的，链表类型依赖
        //先获取到对应任务定义列表，新手任务
        var assignmentDefines = context.AllAssignmentDefine.Where(x => x.AssignmentType == AssignmentTypeEnum.Novice)
            .ToList();

        //根路径
        var rootAssignmentDefine = assignmentDefines.Where(x => x.PreAssignmentId == null).FirstOrDefault();
        //代表没有定义新手任务
        if (rootAssignmentDefine is null)
        {
            return output;
        }

        //1：查询该用户有正在进行的新手任务，如果有跳过
        if (context.CurrentUserAssignments.Any(x => x.AssignmentState == AssignmentStateEnum.Progress))
        {
            return output;
        }

        //2: 查询该用户是否有完成的新手任务，如果有，则根据链表选择最后的节点
        var assignmentFilterIds = context.CurrentUserAssignments
            .Where(x =>
                //已经完成的
                x.AssignmentState == AssignmentStateEnum.Completed
            )
            .Select(x => x.AssignmentDefineId)
            .ToList();

        //该用户接受的最后一个新手任务
        var lastAssignment = assignmentDefines.Where(x => assignmentFilterIds.Contains(x.Id))
            .OrderByDescending(x => x.OrderNum).First();

        //包含比该用户还要大的任务
        if (assignmentDefines.Any(x => x.OrderNum > lastAssignment.OrderNum))
        {
            output.Add(assignmentDefines.FirstOrDefault(x => x.OrderNum == lastAssignment.OrderNum + 1));
        }

        return output;
    }
}