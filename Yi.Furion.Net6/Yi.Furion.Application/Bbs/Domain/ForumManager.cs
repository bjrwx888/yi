using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Core.Bbs.Entities;

namespace Yi.Furion.Application.Bbs.Domain
{
    /// <summary>
    /// 论坛模块的领域服务
    /// </summary>
    public class ForumManager:ITransient
    {
        private readonly IRepository<DiscussEntity> _discussRepository;
        private readonly IRepository<PlateEntity> _plateEntityRepository;
        private readonly IRepository<CommentEntity> _commentRepository;
        public ForumManager(IRepository<DiscussEntity> discussRepository, IRepository<PlateEntity> plateEntityRepository, IRepository<CommentEntity> commentRepository)
        {
            _discussRepository = discussRepository;
            _plateEntityRepository = plateEntityRepository;
            _commentRepository = commentRepository;
        }

        //主题是不能直接创建的，需要由领域服务统一创建
        public async Task<DiscussEntity> CreateDiscussAsync(DiscussEntity entity)
        {
            entity.Id = SnowflakeHelper.NextId;
            entity.CreationTime = DateTime.Now;
            entity.AgreeNum = 0;
            entity.SeeNum = 0;
            return await _discussRepository.InsertReturnEntityAsync(entity);
        }

        public async Task<CommentEntity> CreateCommentAsync(long discussId, long parentId, long rootId, string content)
        {
            var entity = new CommentEntity(discussId);
            entity.Id = SnowflakeHelper.NextId;
            entity.Content = content;
            entity.ParentId = parentId;
            entity.RootId = rootId;
            return await _commentRepository.InsertReturnEntityAsync(entity);
        }
    }
}
