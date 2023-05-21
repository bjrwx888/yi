using System.Linq.Expressions;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Framework.Infrastructure.Enums;

namespace Yi.Framework.Infrastructure.Ddd.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// 注释一下，严格意义这里应该protected，但是我认为 简易程度 与 耦合程度 中是需要进行衡量的
        /// </summary>
        ISugarQueryable<T> _DbQueryable { get; }
        ISqlSugarClient _Db { get; }
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
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, int pageNum, int pageSize);
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, int pageNum, int pageSize, Expression<Func<T, object>>? orderByExpression = null, OrderByEnum orderByType = OrderByEnum.Asc);
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, int pageNum, int pageSize, string? orderBy, OrderByEnum orderByType = OrderByEnum.Asc);
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
        Task<bool> UpdateIgnoreNullAsync(T updateObj);

        //删除
        Task<bool> DeleteAsync(T deleteObj);
        Task<bool> DeleteAsync(List<T> deleteObjs);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);
        Task<bool> DeleteByIdAsync(dynamic id);
        Task<bool> DeleteByIdsAsync(dynamic[] ids);

    }
}
