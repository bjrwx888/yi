using SqlSugar;
using System.Threading.Tasks;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class AgreeService : BaseService<AgreeEntity>, IAgreeService
    {
        /// <summary>
        /// 点赞操作
        /// </summary>
        /// <returns></returns>
        public async Task<bool> OperateAsync(long articleId, long userId)
        {
            var _articleRepositoty = _repository.ChangeRepository<Repository<ArticleEntity>>();
            var article = await _articleRepositoty.GetByIdAsync(articleId);
            if (await _repository.IsAnyAsync(u => u.UserId == userId && u.ArticleId == articleId))
            {
                //已点赞，取消点赞
                await _repository.UseTranAsync(async () =>
                {
                    await _repository.DeleteAsync(u => u.UserId == userId && u.ArticleId == articleId);
                    await _articleRepositoty.UpdateIgnoreNullAsync(new ArticleEntity { Id = articleId, AgreeNum = article.AgreeNum - 1 });

                });
                return false;
            }
            else
            {
                //未点赞，添加点赞记录
                await _repository.UseTranAsync(async () =>
                {
                    await _repository.InsertReturnSnowflakeIdAsync(new AgreeEntity { UserId = userId, ArticleId = articleId });
                    await _articleRepositoty.UpdateIgnoreNullAsync(new ArticleEntity { Id = articleId, AgreeNum = article.AgreeNum + 1 });
                });
                return true;
            }
        }
    }
}
