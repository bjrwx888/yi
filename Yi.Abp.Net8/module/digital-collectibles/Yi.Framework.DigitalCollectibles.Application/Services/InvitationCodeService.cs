using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;
using Yi.Framework.DigitalCollectibles.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.DigitalCollectibles.Application.Services;

/// <summary>
/// 邀请码应用服务
/// </summary>
public class InvitationCodeService : ApplicationService
{
    private readonly ISqlSugarRepository<InvitationCodeAggregateRoot> _repository;

    public InvitationCodeService(ISqlSugarRepository<InvitationCodeAggregateRoot> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 查询当前登录用户的邀请码数据
    /// </summary>
    /// <returns></returns>
    [Authorize]
    public async Task<object> GetAsync()
    {
        var userId = CurrentUser.GetId();
        var entity = await _repository.GetFirstAsync(x => x.UserId == userId);
        if (entity is null)
        {
            return new { IsInvited=false, PointsNumber=0 };
        }

        return new { entity.IsInvited, entity.PointsNumber };
    }

    /// <summary>
    /// 当前用户填写邀请码
    /// </summary>
    /// <param name="invitedUserId"></param>
    /// <exception cref="UserFriendlyException"></exception>
    [Authorize]
    public async Task SetAsync([FromQuery] Guid invitedUserId)
    {
        var userId = CurrentUser.GetId();
        var entity = await _repository.GetFirstAsync(x => x.UserId == userId);
        if (entity is null)
        {
            await _repository.InsertAsync(new InvitationCodeAggregateRoot
            {
                UserId = userId,
                IsInvited = true,
                PointsNumber = 0
            });
        }
        else
        {
            if (entity.IsInvited)
            {
                throw new UserFriendlyException("你已填写过邀请码，无法再次填写");
            }
            else
            {
                entity.IsInvited = false;
                await _repository.UpdateAsync(entity);
            }
        }


        var invitedEntity = await _repository.GetFirstAsync(x => x.UserId == invitedUserId);

        if (entity is null)
        {
            await _repository.InsertAsync(new InvitationCodeAggregateRoot
            {
                UserId = invitedUserId,
                IsInvited = false,
                PointsNumber = 1
            });
        }
        else
        {
            invitedEntity.PointsNumber += 1;
            await _repository.UpdateAsync(invitedEntity);
        }
    }
}