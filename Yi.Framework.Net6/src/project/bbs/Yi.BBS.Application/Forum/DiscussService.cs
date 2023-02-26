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

        /// <summary>
        /// 获取改板块下的主题
        /// </summary>
        /// <param name="plateId"></param>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<DiscussGetListOutputDto>> GetPlateIdAsync([FromRoute] long plateId, [FromQuery] DiscussGetListInputVo input)
        {
            var entities = await _repository.GetPageListAsync(x => x.PlateId == plateId, input);
            var items = await MapToGetListOutputDtosAsync(entities);
            var total = await _repository.CountAsync(x => x.IsDeleted == false);
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
