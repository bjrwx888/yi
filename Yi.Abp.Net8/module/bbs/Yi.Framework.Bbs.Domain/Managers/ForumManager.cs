using Volo.Abp.Domain.Services;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Domain.Managers
{
    /// <summary>
    /// 论坛模块的领域服务
    /// </summary>
    public class ForumManager : DomainService
    {
        public readonly ISqlSugarRepository<DiscussEntity,Guid> _discussRepository;
        public readonly ISqlSugarRepository<PlateEntity, Guid> _plateEntityRepository;
        public readonly ISqlSugarRepository<CommentEntity, Guid> _commentRepository;
        public ForumManager(ISqlSugarRepository<DiscussEntity, Guid> discussRepository, ISqlSugarRepository<PlateEntity, Guid> plateEntityRepository, ISqlSugarRepository<CommentEntity, Guid> commentRepository)
        {
            _discussRepository = discussRepository;
            _plateEntityRepository = plateEntityRepository;
            _commentRepository = commentRepository;
        }

        //主题是不能直接创建的，需要由领域服务统一创建
        public async Task<DiscussEntity> CreateDiscussAsync(DiscussEntity entity)
        {
            entity.CreationTime = DateTime.Now;
            entity.AgreeNum = 0;
            entity.SeeNum = 0;
            return await _discussRepository.InsertReturnEntityAsync(entity);
        }

        public async Task<CommentEntity> CreateCommentAsync(Guid discussId, Guid parentId, Guid rootId, string content)
        {
            var entity = new CommentEntity(discussId);
            entity.Content = content;
            entity.ParentId = parentId;
            entity.RootId = rootId;
            return await _commentRepository.InsertReturnEntityAsync(entity);
        }
    }
}
