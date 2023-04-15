using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Furion.Core.Bbs.Entities;

namespace Yi.Furion.Sqlsugar.Core.Repositories
{
    public interface IArticleRepository : IRepository<ArticleEntity>
    {
        Task<List<ArticleEntity>> GetTreeAsync(Expression<Func<ArticleEntity, bool>> where);
    }
}
