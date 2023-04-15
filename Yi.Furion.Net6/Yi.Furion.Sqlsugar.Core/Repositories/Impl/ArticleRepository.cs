using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Yi.Framework.Infrastructure.Sqlsugar.Repositories;
using Yi.Furion.Core.Bbs.Entities;

namespace Yi.Furion.Sqlsugar.Core.Repositories.Impl
{
    public class ArticleRepository : SqlsugarRepository<ArticleEntity>, IArticleRepository,ITransient
    {
        public ArticleRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<List<ArticleEntity>> GetTreeAsync(Expression<Func<ArticleEntity, bool>> where)
        {
            return await _DbQueryable.Where(where).ToTreeAsync(x => x.Children, x => x.ParentId, 0);
        }
    }
}
