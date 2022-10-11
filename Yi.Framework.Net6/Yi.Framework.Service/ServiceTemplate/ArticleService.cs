using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class ArticleService : BaseService<ArticleEntity>, IArticleService
    {
        public ArticleService(IRepository<ArticleEntity> repository) : base(repository)
        {
        }
    }
}
