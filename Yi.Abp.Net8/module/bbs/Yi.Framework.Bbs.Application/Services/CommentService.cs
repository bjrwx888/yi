using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Comment;
using Yi.Framework.Bbs.Application.Contracts.IServices;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Managers;
using Yi.Framework.Bbs.Domain.Shared.Consts;
using Yi.Framework.Ddd.Application;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Application.Services
{
    /// <summary>
    /// 评论
    /// </summary>
    public class CommentService : YiCrudAppService<CommentEntity, CommentGetOutputDto, CommentGetListOutputDto, Guid, CommentGetListInputVo, CommentCreateInputVo, CommentUpdateInputVo>,
       ICommentService
    {
        private readonly ISqlSugarRepository<CommentEntity, Guid> _repository;
        public CommentService(ForumManager forumManager, ISqlSugarRepository<DiscussEntity> discussRepository, IDiscussService discussService, ISqlSugarRepository<CommentEntity, Guid> CommentRepository) : base(CommentRepository)
        {
            _forumManager = forumManager;
            _discussRepository = discussRepository;
            _discussService = discussService;
            _repository = CommentRepository;
        }

        private ForumManager _forumManager { get; set; }



        private ISqlSugarRepository<DiscussEntity> _discussRepository { get; set; }

        private IDiscussService _discussService { get; set; }
        /// <summary>
        /// 获取改主题下的评论,结构为二维列表，该查询无分页
        /// </summary>
        /// <param name="discussId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CommentGetListOutputDto>> GetDiscussIdAsync([FromRoute] Guid discussId, [FromQuery] CommentGetListInputVo input)
        {
            await _discussService.VerifyDiscussPermissionAsync(discussId);

            var entities = await _repository._DbQueryable.WhereIF(!string.IsNullOrEmpty(input.Content), x => x.Content.Contains(input.Content))
              .Where(x => x.DiscussId == discussId)
              .Includes(x => x.CreateUser)
             .ToListAsync();

            //结果初始值，第一层等于全部根节点
            var outPut = entities.Where(x => x.ParentId == Guid.Empty).OrderByDescending(x => x.CreationTime).ToList();

            //将全部数据进行hash
            var dic = entities.ToDictionary(x => x.Id);


            foreach (var comment in entities)
            {
                //不是根节点，需要赋值 被评论者用户信息等
                if (comment.ParentId != Guid.Empty)
                {
                    if (dic.ContainsKey(comment.ParentId))
                    {
                        var parentComment = dic[comment.ParentId];
                        comment.CommentedUser = parentComment.CreateUser;
                    }
                    else
                    {
                        continue;
                    }
                }

                //root或者parent id，根节点都是等于0的
                var id = comment.RootId;
                if (id != Guid.Empty)
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
            List<CommentGetListOutputDto> items = await MapToGetListOutputDtosAsync(outPut);
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
                throw new UserFriendlyException(DiscussConst.No_Exist);
            }
            var entity = await _forumManager.CreateCommentAsync(input.DiscussId, input.ParentId, input.RootId, input.Content);
            return await MapToGetOutputDtoAsync(entity);
        }

    }
}
