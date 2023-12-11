﻿using System.Linq.Expressions;
using Volo.Abp.DependencyInjection;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Repositories;
using Yi.Framework.SqlSugarCore.Abstractions;
using Yi.Framework.SqlSugarCore.Repositories;

namespace Yi.Framework.Bbs.SqlSugarCore.Repositories
{
    public class ArticleRepository : SqlSugarRepository<ArticleEntity,Guid>, IArticleRepository,ITransientDependency
    {
        public ArticleRepository(ISugarDbContextProvider<ISqlSugarDbContext> sugarDbContextProvider) : base(sugarDbContextProvider)
        {
        }

        public async Task<List<ArticleEntity>> GetTreeAsync(Expression<Func<ArticleEntity, bool>> where)
        {
            return await _DbQueryable.Where(where).ToTreeAsync(x => x.Children, x => x.ParentId, 0);
        }
    }
}
