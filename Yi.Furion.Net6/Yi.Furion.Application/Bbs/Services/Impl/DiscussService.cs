using Furion.EventBus;
using SqlSugar;
using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Infrastructure.Exceptions;
using Yi.Furion.Application.Bbs.Domain;
using Yi.Furion.Core.Bbs.Consts;
using Yi.Furion.Core.Bbs.Dtos.Discuss;
using Yi.Furion.Core.Bbs.Entities;
using Yi.Furion.Core.Bbs.Enums;
using Yi.Furion.Core.Bbs.Etos;
using Yi.Furion.Core.Rbac.Dtos.User;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Application.Bbs.Services.Impl
{
    /// <summary>
    /// Discuss应用服务实现,用于参数效验、领域服务业务组合、日志记录、事务处理、账户信息
    /// </summary>
    [ApiDescriptionSettings("BBS")]
    public class DiscussService : CrudAppService<DiscussEntity, DiscussGetOutputDto, DiscussGetListOutputDto, long, DiscussGetListInputVo, DiscussCreateInputVo, DiscussUpdateInputVo>,
       IDiscussService,IDynamicApiController,ITransient
    {
        public DiscussService(ICurrentUser currentUser, ForumManager forumManager, IRepository<PlateEntity> plateEntityRepository, IEventPublisher eventPublisher)
        {
            _currentUser = currentUser;
            _forumManager = forumManager;
            _plateEntityRepository= plateEntityRepository;
            _eventPublisher= eventPublisher;
        }

        private ForumManager _forumManager { get; set; }


        private IRepository<PlateEntity> _plateEntityRepository { get; set; }

        private IEventPublisher _eventPublisher { get; set; }


        //[Autowired]
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
                await VerifyDiscussPermissionAsync(item.Id);
                _eventPublisher.PublishAsync(new SeeDiscussEventSource(new SeeDiscussEventArgs { DiscussId = item.Id, OldSeeNum = item.SeeNum }));
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

                     .LeftJoin<UserEntity>((discuss, user) => discuss.CreatorId == user.Id)
                      .OrderByIF(input.Type == QueryDiscussTypeEnum.New, discuss => discuss.CreationTime, OrderByType.Desc)
                     .OrderByIF(input.Type == QueryDiscussTypeEnum.Host, discuss => discuss.SeeNum, OrderByType.Desc)
                      .OrderByIF(input.Type == QueryDiscussTypeEnum.Suggest, discuss => discuss.AgreeNum, OrderByType.Desc)
                     .Select((discuss, user) => new DiscussGetListOutputDto
                     {
                         Id = discuss.Id,
                         IsAgree = SqlFunc.Subqueryable<AgreeEntity>().Where(x => x.CreatorId == _currentUser.Id && x.DiscussId == discuss.Id).Any(),

                         User = new UserGetListOutputDto() { Id = user.Id, UserName = user.UserName, Nick = user.Nick, Icon = user.Icon }

                     }, true)
                .ToPageListAsync(input.PageNum, input.PageSize, total);

            //查询完主题之后，要过滤一下私有的主题信息
            items.ApplyPermissionTypeFilter(_currentUser.Id);
            return new PagedResultDto<DiscussGetListOutputDto>(total, items);
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
        public async Task VerifyDiscussPermissionAsync(long discussId)
        {
            var discuss = await _repository.GetFirstAsync(x => x.Id == discussId);
            if (discuss is null)
            {
                throw new UserFriendlyException(DiscussConst.No_Exist);
            }
            if (discuss.PermissionType == DiscussPermissionTypeEnum.Oneself)
            {
                if (discuss.CreatorId != _currentUser.Id)
                {
                    throw new UserFriendlyException(DiscussConst.Privacy);
                }
            }
            if (discuss.PermissionType == DiscussPermissionTypeEnum.User)
            {
                if (discuss.CreatorId != _currentUser.Id && !discuss.PermissionUserIds.Contains(_currentUser.Id))
                {
                    throw new UserFriendlyException(DiscussConst.Privacy);
                }
            }
        }
    }
}
