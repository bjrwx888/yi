using Yi.BBS.Application.Contracts.Forum;
using Cike.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Ddd.Services;
using Yi.BBS.Application.Contracts.Forum.Dtos.Discuss;
using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Ddd.Dtos;
using Yi.BBS.Domain.Forum;
using MapsterMapper;
using SqlSugar;
using Microsoft.AspNetCore.Routing;
using Yi.BBS.Domain.Shared.Forum.ConstClasses;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Cike.EventBus.DistributedEvent;
using Yi.BBS.Domain.Shared.Forum.Etos;
using Yi.BBS.Domain.Shared.Forum.EnumClasses;
using Yi.Framework.Core.CurrentUsers;
using Yi.BBS.Domain.Exhibition.Entities;

namespace Yi.BBS.Application.Forum
{
    /// <summary>
    /// Discuss应用服务实现,用于参数效验、领域服务业务组合、日志记录、事务处理、账户信息
    /// </summary>
    [AppService]
    public class DiscussService : CrudAppService<DiscussEntity, DiscussGetOutputDto, DiscussGetListOutputDto, long, DiscussGetListInputVo, DiscussCreateInputVo, DiscussUpdateInputVo>,
       IDiscussService, IAutoApiService
    {
        [Autowired]
        private ForumManager _forumManager { get; set; }

        [Autowired]
        private IRepository<PlateEntity> _plateEntityRepository { get; set; }

        [Autowired]
        private IDistributedEventBus _distributedEventBus { get; set; }

        [Autowired]
        private ICurrentUser _currentUser { get; set; }
        /// <summary>
        /// 单查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async override Task<DiscussGetOutputDto> GetAsync(long id)
        {
            //查询主题发布 浏览主题 事件，浏览数+1
            var item = await _DbQueryable.LeftJoin<UserEntity>((discuss, user) => discuss.CreatorId == user.Id)
                     .Select((discuss, user) => new DiscussGetOutputDto
                     {
                         User = new UserGetListOutputDto() { UserName = user.UserName, Nick = user.Nick, Icon = user.Icon }
                     }, true).SingleAsync(discuss => discuss.Id == id);
            if (item is not null)
            {
                _distributedEventBus.PublishAsync(new SeeDiscussEventArgs { DiscussId = item.Id, OldSeeNum = item.SeeNum });
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
            var items = await _DbQueryable
                 .WhereIF(!string.IsNullOrEmpty(input.Title), x => x.Title.Contains(input.Title))
                     .WhereIF(input.PlateId is not null, x => x.PlateId == input.PlateId)
                     .Where(x => x.IsTop == input.IsTop)
                     .OrderByIF(input.Type == QueryDiscussTypeEnum.New, x => x.CreationTime, OrderByType.Desc)
                     .OrderByIF(input.Type == QueryDiscussTypeEnum.Host, x => x.SeeNum, OrderByType.Desc)
                      .OrderByIF(input.Type == QueryDiscussTypeEnum.Suggest, x => x.AgreeNum, OrderByType.Desc)
                     .LeftJoin<UserEntity>((discuss, user) => discuss.CreatorId == user.Id)
                     .Select((discuss, user) => new DiscussGetListOutputDto
                     {
                         Id=discuss.Id,
                         IsAgree = SqlFunc.Subqueryable<AgreeEntity>().Where(x => x.CreatorId == _currentUser.Id && x.DiscussId == discuss.Id).Any(),
                         User = new UserGetListOutputDto() { UserName = user.UserName, Nick = user.Nick, Icon = user.Icon }
                     }, true)
                .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<DiscussGetListOutputDto>(total, items);
        }

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<DiscussGetOutputDto> CreateAsync(DiscussCreateInputVo input)
        {
            if (!await _plateEntityRepository.IsAnyAsync(x => x.Id == input.plateId))
            {
                throw new UserFriendlyException(PlateConst.板块不存在);
            }
            var entity = await _forumManager.CreateDiscussAsync(input.plateId, input.Title, input.Types, input.Content, input.Introduction);
            return await MapToGetOutputDtoAsync(entity);
        }
    }
}
