using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Domain.Forum.Entities;
using Yi.BBS.Domain.Forum.Repositories;
using Yi.Framework.Core.Sqlsugar.Repositories;
using Yi.Framework.Ddd.Repositories;

namespace Yi.BBS.Sqlsugar.Repositories
{
    [AppService]
    public class ArticleRepository : SqlsugarRepository<ArticleEntity>, IArticleRepository
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
