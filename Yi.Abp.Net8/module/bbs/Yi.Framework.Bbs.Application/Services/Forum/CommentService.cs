using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Yi.Framework.Bbs.Application.Contracts.Dtos.BbsUser;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Comment;
using Yi.Framework.Bbs.Application.Contracts.IServices;
using Yi.Framework.Bbs.Domain.Entities.Forum;
using Yi.Framework.Bbs.Domain.Managers;
using Yi.Framework.Bbs.Domain.Shared.Consts;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Domain.Authorization;
using Yi.Framework.Rbac.Domain.Extensions;
using Yi.Framework.Rbac.Domain.Shared.Consts;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Application.Services.Forum
{
    /// <summary>
    /// 评论
    /// </summary>
    public class CommentService : ApplicationService,
       ICommentService
    {
        private readonly ISqlSugarRepository<CommentAggregateRoot, Guid> _repository;
        private readonly BbsUserManager _bbsUserManager;

        private ForumManager _forumManager { get; set; }



        private ISqlSugarRepository<DiscussAggregateRoot> _discussRepository { get; set; }

        private IDiscussService _discussService { get; set; }


        public  async Task<CommentGetOutputDto> Create2Async(CommentCreateInputVo input)
        {
            var entity = new CommentAggregateRoot(Guid.Empty);
            return new CommentGetOutputDto();
        }

        [HttpGet("Create22")]
        public  async Task<CommentGetOutputDto> Create22Async(CommentCreateInputVo input)
        {
            var entity = new CommentAggregateRoot(Guid.Empty);
            return new CommentGetOutputDto();
        }
        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        // [Permission("bbs:comment:add")]
        // [Authorize]
        public  async Task<CommentGetOutputDto> CreateAsync(CommentCreateInputVo input)
        {
            // var discuess = await _discussRepository.GetFirstAsync(x => x.Id == input.DiscussId);
            // if (discuess is null)
            // {
            //     throw new UserFriendlyException(DiscussConst.No_Exist);
            // }
            //不是超级管理员，且主题开启禁止评论

            // if (discuess.IsDisableCreateComment == true && !CurrentUser.GetPermissions().Contains(UserConst.AdminPermissionCode))
            // {
            //     throw new UserFriendlyException("该主题已禁止评论功能");
            // }


           // var entity = await _forumManager.CreateCommentAsync(input.DiscussId, input.ParentId, input.RootId, input.Content);
           var entity = new CommentAggregateRoot(Guid.Empty);
           return new CommentGetOutputDto();
        }

        public CommentService(IRepository<CommentAggregateRoot, Guid> repository) 
        {
        }
    }
}
