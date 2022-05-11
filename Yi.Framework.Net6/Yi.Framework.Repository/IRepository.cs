using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Models;
using Yi.Framework.Model.Query;

namespace Yi.Framework.Repository
{
    public interface IRepository<T> : ISimpleClient<T> where T : class, IBaseModelEntity, new()
    {
        public ISqlSugarClient _Db { get; set; }
        public Task<bool> UseTranAsync(Func<Task> func);
        public Task<T> InsertReturnEntityAsync(T entity);
        public Task<List<S>> StoreAsync<S>(string storeName, object para);
        public Task<PageModel<List<T>>> CommonPageAsync(QueryPageCondition pars);
        public  Task<List<T>> GetListAsync(QueryCondition pars);
        public Task<bool> DeleteByLogicAsync(List<long> ids);
        public  Task<bool> UpdateIgnoreNullAsync(T entity);
        public  Task<List<S>> UseSqlAsync<S>(string sql, object parameters = null);
        public  Task<bool> UseSqlAsync(string sql, object parameters = null);
        ISugarQueryable<T> QueryConditionHandler(QueryCondition pars);
    }
}
