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
    public interface IRepository<T> : ISimpleClient<T> where T : BaseModelEntity,new()
    {
        public Task<T> InsertReturnEntityAsync(T entity);
        public Task<List<S>> StoreAsync<S>(string storeName, object para);
        public Task<PageModel<List<T>>> CommonPage(QueryPageCondition pars);
        public  Task<List<T>> GetListAsync(QueryCondition pars);
        public Task<bool> DeleteByLogic(List<Guid> ids);
    }
}
