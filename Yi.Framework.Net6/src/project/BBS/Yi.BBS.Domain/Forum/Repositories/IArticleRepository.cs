using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Ddd.Repositories;

namespace Yi.BBS.Domain.Forum.Repositories
{
    public interface IArticleRepository:IRepository<ArticleEntity>
    {
        Task<List<ArticleEntity>> GetTreeAsync(Expression<Func<ArticleEntity, bool>> where);
    }
}
