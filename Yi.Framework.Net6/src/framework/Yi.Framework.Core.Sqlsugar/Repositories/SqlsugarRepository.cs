using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.Enums;
using Yi.Framework.Core.Helper;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Ddd.Entities;
using Yi.Framework.Ddd.Repositories;

namespace Yi.Framework.Core.Sqlsugar.Repositories
{
    [AppService(typeof(IRepository<>))]
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

        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, IPagedAndSortedResultRequestDto page, string? orderBy, OrderByEnum orderByType = OrderByEnum.Asc)
        {
            return await _DbQueryable.Where(whereExpression).OrderByIF(orderBy is not null, orderBy + " " + orderByType.ToString().ToLower()).ToPageListAsync(page.PageIndex, page.PageSize);
        }


        public async Task<bool> UpdateIgnoreNullAsync(T updateObj)
        {
            return await _Db.Updateable(updateObj).IgnoreColumns(true).ExecuteCommandAsync() > 0;
        }

        public override async Task<bool> DeleteAsync(T deleteObj)
        {
            //逻辑删除
            if (deleteObj is ISoftDelete)
            {
                //反射赋值
                ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, deleteObj);
                return await UpdateAsync(deleteObj);

            }
            else
            {
                return await base.DeleteAsync(deleteObj);

            }
        }
        public override async Task<bool> DeleteAsync(List<T> deleteObjs)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                //反射赋值
                deleteObjs.ForEach(e => ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, e));
                return await UpdateRangeAsync(deleteObjs);
            }
            else
            {
                return await base.DeleteAsync(deleteObjs);
            }
        }
        public override async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                var entities = await GetListAsync(whereExpression);
                //反射赋值
                entities.ForEach(e => ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, e));
                return await UpdateRangeAsync(entities);
            }
            else
            {
                return await base.DeleteAsync(whereExpression);
            }
        }
        public override async Task<bool> DeleteByIdAsync(dynamic id)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {   
                var entity = await GetByIdAsync(id);
                //反射赋值
                ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, entity);
                return await UpdateAsync(entity);
            }
            else
            {
                return await _Db.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
            }

        }
        public override async Task<bool> DeleteByIdsAsync(dynamic[] ids)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                var entities = await _DbQueryable.In(ids).ToListAsync();
                if (entities.Count == 0)
                {
                    return false;
                }
                //反射赋值
                entities.ForEach(e => ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, e));
                return await UpdateRangeAsync(entities);
            }
            else
            {
                return await base.DeleteByIdsAsync(ids);
            }

        }

    }
}
