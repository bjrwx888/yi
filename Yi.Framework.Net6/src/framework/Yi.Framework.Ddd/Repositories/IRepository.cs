using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Enums;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Ddd.Entities;

namespace Yi.Framework.Ddd.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        //单查
        Task<T> GetByIdAsync(dynamic id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression);
        Task<bool> IsAnyAsync(Expression<Func<T, bool>> whereExpression);
        Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);

        //多查
        Task<List<T>> GetListAsync();
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression);

        //分页查
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, IPagedAndSortedResultRequestDto page);
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, IPagedAndSortedResultRequestDto page, Expression<Func<T, object>>? orderByExpression = null, OrderByEnum orderByType = OrderByEnum.Asc);
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, IPagedAndSortedResultRequestDto page, string? orderBy, OrderByEnum orderByType = OrderByEnum.Asc);


        //插入
        Task<bool> InsertAsync(T insertObj);
        Task<bool> InsertOrUpdateAsync(T data);
        Task<bool> InsertOrUpdateAsync(List<T> datas);
        Task<int> InsertReturnIdentityAsync(T insertObj);
        Task<long> InsertReturnBigIdentityAsync(T insertObj);
        Task<long> InsertReturnSnowflakeIdAsync(T insertObj);
        Task<T> InsertReturnEntityAsync(T insertObj);
        Task<bool> InsertRangeAsync(List<T> insertObjs);

        //更新
        Task<bool> UpdateAsync(T updateObj);
        Task<bool> UpdateRangeAsync(List<T> updateObjs);
        Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);

        //删除
        Task<bool> DeleteAsync(T deleteObj);
        Task<bool> DeleteAsync(List<T> deleteObjs);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);
        Task<bool> DeleteByIdAsync(dynamic id);
        Task<bool> DeleteByIdsAsync(dynamic[] ids);
    }
}
