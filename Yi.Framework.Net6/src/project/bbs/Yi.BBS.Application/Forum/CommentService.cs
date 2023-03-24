using Yi.BBS.Application.Contracts.Forum;
using Cike.AutoWebApi.Setting;
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
using SqlSugar;
using System.Security.AccessControl;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Yi.BBS.Application.Forum
{
    /// <summary>
    /// 评论
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
        /// 获取改主题下的评论,结构为二维列表，该查询无分页
        /// </summary>
        /// <param name="discussId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CommentGetListOutputDto>> GetDiscussIdAsync([FromRoute] long discussId, [FromQuery] CommentGetListInputVo input)
        {

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.Content), x => x.Content.Contains(input.Content))
              .Where(x => x.DiscussId == discussId)
              .Includes(x=>x.CreateUser)
             .ToListAsync();

            //结果初始值，第一层等于全部根节点
          var outPut = entities.Where(x => x.ParentId == 0).OrderByDescending(x=>x.CreationTime).ToList();

            //将全部数据进行hash
            var dic = entities.ToDictionary(x => x.Id);


            foreach (var comment in entities)
            {
                //不是根节点，需要赋值 被评论者用户信息等
                if (comment.ParentId != 0)
                {
                    var parentComment = dic[comment.ParentId];
                    comment.CommentedUser = parentComment.CreateUser;
                }

                //root或者parent id，根节点都是等于0的
                var id = comment.RootId;
                if (id is not 0)
                {
                    dic[id].Children.Add(comment);
                }

            }

            //子类需要排序
            outPut.ForEach(x =>
            {
                x.Children = x.Children.OrderByDescending(x => x.CreationTime).ToList();

            });

            //获取全量主题评论， 先获取顶级的，将其他子组合到顶级下，形成一个二维,先转成dto
            List<CommentGetListOutputDto>? items = await MapToGetListOutputDtosAsync(outPut);
            return new PagedResultDto<CommentGetListOutputDto>(entities.Count(), items);
        }


        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<CommentGetOutputDto> CreateAsync(CommentCreateInputVo input)
        {
            if (!await _discussRepository.IsAnyAsync(x => x.Id == input.DiscussId))
            {
                throw new UserFriendlyException(DiscussConst.主题不存在);
            }
            var entity = await _forumManager.CreateCommentAsync(input.DiscussId, input.ParentId,input.RootId, input.Content);
            return await MapToGetOutputDtoAsync(entity);
        }

    }
}
