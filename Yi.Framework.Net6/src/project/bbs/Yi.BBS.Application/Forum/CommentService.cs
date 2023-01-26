using Yi.BBS.Application.Contracts.Forum;
using NET.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Ddd.Services;
using Microsoft.AspNetCore.Mvc;
using Yi.BBS.Domain.Forum;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Ddd.Repositories;
using Yi.BBS.Domain.Shared.Forum.ConstClasses;
using Yi.BBS.Application.Contracts.Forum.Dtos.Discuss;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Forum
{
    /// <summary>
    /// Comment服务实现
    /// </summary>
    [AppService]
    public class CommentService : CrudAppService<CommentEntity, CommentGetOutputDto, CommentGetListOutputDto, long, CommentGetListInputVo, CommentCreateInputVo, CommentUpdateInputVo>,
       ICommentService, IAutoApiService
    {
        [Autowired]
        private ForumManager _forumManager { get; set; }

        [Autowired]
        private ICurrentUser _currentUser { get; set; }

        [Autowired]
        private IRepository<DiscussEntity> _discussRepository { get; set; }


        /// <summary>
        /// 获取改主题下的评论
        /// </summary>
        /// <param name="discussId"></param>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<CommentGetListOutputDto>> GetDiscussIdAsync([FromRoute] long discussId, [FromQuery] CommentGetListInputVo input)
        {
            var entities = await _repository.GetPageListAsync(x => x.DiscussId == discussId, input);
            var items = await MapToGetListOutputDtosAsync(entities);
            var total = await _repository.CountAsync(x => x.IsDeleted == false);
            return new PagedResultDto<CommentGetListOutputDto>(total, items);
        }


        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<CommentGetOutputDto> CreateAsync(CommentCreateInputVo input)
        {
            //if (_currentUser.Id == default(long))
            //{
            //    throw new UserFriendlyException("用户不存在");
            //}
            if (!await _discussRepository.IsAnyAsync(x => x.Id == input.DiscussId))
            {
                throw new UserFriendlyException(DiscussConst.主题不存在);

            }
            var entity = await _forumManager.CreateCommentAsync(input.DiscussId, _currentUser.Id, input.Content);
            return await MapToGetOutputDtoAsync(entity);
        }

    }
}
