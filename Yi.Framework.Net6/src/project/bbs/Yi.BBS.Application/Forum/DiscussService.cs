using Yi.BBS.Application.Contracts.Forum;
using NET.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Ddd.Services;
using Yi.BBS.Application.Contracts.Forum.Dtos.Discuss;
using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Ddd.Dtos;
using Yi.BBS.Domain.Forum;
using AutoMapper;
using SqlSugar;
using Microsoft.AspNetCore.Routing;

namespace Yi.BBS.Application.Forum
{
    /// <summary>
    /// Discuss服务实现
    /// </summary>
    [AppService]
    public class DiscussService : CrudAppService<DiscussEntity, DiscussGetOutputDto, DiscussGetListOutputDto, long, DiscussGetListInputVo, DiscussCreateInputVo, DiscussUpdateInputVo>,
       IDiscussService, IAutoApiService
    {
        private readonly ForumManager _forumManager;
        public DiscussService(ForumManager forumManager)
        {
            _forumManager = forumManager;
        }

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

        [AutoApi(false)]
        public override Task<DiscussGetOutputDto> CreateAsync(DiscussCreateInputVo input)
        {
            return base.CreateAsync(input);
        }

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="plateId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<DiscussGetOutputDto> CreatePlateIdAsync([FromRoute] long plateId, DiscussCreateInputVo input)
        {
            var entity = await _forumManager.CreateDiscussAsync(plateId, input.Title, input.Types, input.Content, input.Introduction);
            return await MapToGetOutputDtoAsync(entity);
        }
    }
}
