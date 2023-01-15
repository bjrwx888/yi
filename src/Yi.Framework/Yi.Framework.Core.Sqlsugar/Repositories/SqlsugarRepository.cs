using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.Enums;
using Yi.Framework.Core.Helper;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Ddd.Repositories;

namespace Yi.Framework.Core.Sqlsugar.Repositories
{
    [AppService(ServiceType = typeof(IRepository<>))]
    public class SqlsugarRepository<T> : SimpleClient<T>, IRepository<T> where T : class, new()
    {
        public SqlsugarRepository(ISqlSugarClient context) : base(context)
        {
        }
        protected ISugarQueryable<T> _DbQueryable { get { return base.AsQueryable(); } set { } }

        protected ISqlSugarClient _Db { get { return Context; } set { } }

        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, IPagedAndSortedResultRequestDto page)
        {
            return await base.GetPageListAsync(whereExpression, new PageModel { PageIndex = page.PageIndex, PageSize = page.PageSize });
        }

        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, IPagedAndSortedResultRequestDto page, Expression<Func<T, object>>? orderByExpression = null, OrderByEnum orderByType = OrderByEnum.Asc)
        {
            return await base.GetPageListAsync(whereExpression, new PageModel { PageIndex = page.PageIndex, PageSize = page.PageSize }, orderByExpression, orderByType.EnumToEnum<OrderByType>());
        }
    }
}
