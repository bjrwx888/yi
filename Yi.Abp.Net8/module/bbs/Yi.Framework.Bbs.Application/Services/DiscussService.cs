using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Users;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Discuss;
using Yi.Framework.Bbs.Application.Contracts.IServices;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Managers;
using Yi.Framework.Bbs.Domain.Shared.Consts;
using Yi.Framework.Bbs.Domain.Shared.Enums;
using Yi.Framework.Bbs.Domain.Shared.Etos;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Application.Contracts.Dtos.User;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Application.Services
{
    /// <summary>
    /// Discuss应用服务实现,用于参数效验、领域服务业务组合、日志记录、事务处理、账户信息
    /// </summary>
    public class DiscussService : YiCrudAppService<DiscussEntity, DiscussGetOutputDto, DiscussGetListOutputDto, Guid, DiscussGetListInputVo, DiscussCreateInputVo, DiscussUpdateInputVo>,
       IDiscussService
    {
        private ISqlSugarRepository<DiscussTopEntity> _discussTopEntityRepository;
        public DiscussService(ForumManager forumManager, ISqlSugarRepository<DiscussTopEntity> discussTopEntityRepository, ISqlSugarRepository<PlateEntity> plateEntityRepository, ILocalEventBus localEventBus) : base(forumManager._discussRepository)
        {
            _forumManager = forumManager;
            _plateEntityRepository = plateEntityRepository;
            _localEventBus = localEventBus;
            _discussTopEntityRepository = discussTopEntityRepository;
        }
        private readonly ILocalEventBus _localEventBus;
        private ForumManager _forumManager { get; set; }


        private ISqlSugarRepository<PlateEntity> _plateEntityRepository { get; set; }




        /// <summary>
        /// 单查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async override Task<DiscussGetOutputDto> GetAsync(Guid id)
        {

            //查询主题发布 浏览主题 事件，浏览数+1
            var item = await _forumManager._discussRepository._DbQueryable.LeftJoin<UserEntity>((discuss, user) => discuss.CreatorId == user.Id)
                     .Select((discuss, user) => new DiscussGetOutputDto
                     {
                         User = new UserGetListOutputDto() { UserName = user.UserName, Nick = user.Nick, Icon = user.Icon }
                     }, true).SingleAsync(discuss => discuss.Id == id);

            if (item is not null)
            {
                await VerifyDiscussPermissionAsync(item.Id);
                await _localEventBus.PublishAsync(new SeeDiscussEventArgs { DiscussId = item.Id, OldSeeNum = item.SeeNum });
            }

            return item;
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<DiscussGetListOutputDto>> GetListAsync([FromQuery] DiscussGetListInputVo input)
        {
            //需要关联创建者用户
            RefAsync<int> total = 0;
            var items = await _forumManager._discussRepository._DbQueryable
                 .WhereIF(!string.IsNullOrEmpty(input.Title), x => x.Title.Contains(input.Title))
                     .WhereIF(input.PlateId is not null, x => x.PlateId == input.PlateId)


                     .WhereIF(input.IsTop == true, x => x.IsTop == input.IsTop)

                     .LeftJoin<UserEntity>((discuss, user) => discuss.CreatorId == user.Id)

                         .OrderByDescending(x => x.OrderNum)
                      .OrderByIF(input.Type == QueryDiscussTypeEnum.New, discuss => discuss.CreationTime, OrderByType.Desc)
                     .OrderByIF(input.Type == QueryDiscussTypeEnum.Host, discuss => discuss.SeeNum, OrderByType.Desc)
                      .OrderByIF(input.Type == QueryDiscussTypeEnum.Suggest, discuss => discuss.AgreeNum, OrderByType.Desc)

                     .Select((discuss, user) => new DiscussGetListOutputDto
                     {
                         Id = discuss.Id,
                         IsAgree = SqlFunc.Subqueryable<AgreeEntity>().WhereIF(CurrentUser.Id != null, x => x.CreatorId == CurrentUser.Id && x.DiscussId == discuss.Id).Any(),

                         User = new UserGetListOutputDto() { Id = user.Id, UserName = user.UserName, Nick = user.Nick, Icon = user.Icon }

                     }, true)
                .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);

            //查询完主题之后，要过滤一下私有的主题信息
            items.ApplyPermissionTypeFilter(CurrentUser.Id ?? Guid.Empty);
            return new PagedResultDto<DiscussGetListOutputDto>(total, items);
        }

        /// <summary>
        /// 获取首页的置顶主题
        /// </summary>
        /// <returns></returns>
        public async Task<List<DiscussGetListOutputDto>> GetListTopAsync()
        {
            var entities = await _discussTopEntityRepository._DbQueryable.Includes(x => x.Discuss).OrderByDescending(x => x.OrderNum).ToListAsync();

            var output = await MapToGetListOutputDtosAsync(entities.Select(x => x.Discuss).ToList());
            return output;
        }

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<DiscussGetOutputDto> CreateAsync(DiscussCreateInputVo input)
        {
            if (!await _plateEntityRepository.IsAnyAsync(x => x.Id == input.PlateId))
            {
                throw new UserFriendlyException(PlateConst.No_Exist);
            }
            var entity = await _forumManager.CreateDiscussAsync(await MapToEntityAsync(input));
            return await MapToGetOutputDtoAsync(entity);
        }

        /// <summary>
        /// 效验主题查询权限
        /// </summary>
        /// <param name="discussId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task VerifyDiscussPermissionAsync(Guid discussId)
        {
            var discuss = await _forumManager._discussRepository.GetFirstAsync(x => x.Id == discussId);
            if (discuss is null)
            {
                throw new UserFriendlyException(DiscussConst.No_Exist);
            }
            if (discuss.PermissionType == DiscussPermissionTypeEnum.Oneself)
            {
                if (discuss.CreatorId != CurrentUser.Id)
                {
                    throw new UserFriendlyException(DiscussConst.Privacy);
                }
            }
            if (discuss.PermissionType == DiscussPermissionTypeEnum.User)
            {
                if (discuss.CreatorId != CurrentUser.Id && !discuss.PermissionUserIds.Contains(CurrentUser.Id ?? Guid.Empty))
                {
                    throw new UserFriendlyException(DiscussConst.Privacy);
                }
            }
        }
    }
}
