using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Repository
{
    public interface IRepository<T> : ISimpleClient<T> where T : class, new()
    {
        public Task<T> InsertReturnEntityAsync(T entity);
        public object CommonPage(QueryParameters pars, int pageIndex, int pageSize);
        public object CommonPage(QueryParameters pars, int pageIndex, int pageSize, bool whereBool, Expression<Func<T, bool>> where);

        public object CommonPageMapper<T2, TT>(Expression<Func<TT, ManyToMany>> expression, QueryParameters pars, int pageIndex, int pageSize, bool whereBool, Expression<Func<T, bool>> where);

        public Task<T> FirstMapperAsync<T2, TT>(Expression<Func<TT, ManyToMany>> expression, bool isTenant = true);

        public Task<List<T>> ToListMapperAsync<T2, TT>(Expression<Func<TT, ManyToMany>> expression, bool isTenant = true);

        public Task<List<T>> ToListMapperAsync<T2, TT>(Expression<Func<TT, ManyToMany>> expression, bool whereBool, Expression<Func<T, bool>> where, bool isTenant = true);

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, bool whereBool, Expression<Func<T, bool>> where, bool isTenant = true);

        public Task<List<T>> GetListAsync(bool whereBool, Expression<Func<T, bool>> where, bool isTenant = true);
        public Task<List<S>> StoreAsync<S>(string storeName, object para);

    }
}
