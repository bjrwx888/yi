﻿using SqlSugar;
using System.Data;
using System.Linq.Expressions;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Models;
using Yi.Framework.Model.Query;

/***这里面写的代码不会给覆盖,如果要重新生成请删除 Repository.cs ***/
namespace Yi.Framework.Repository
{
    /// <summary>
    /// 仓储模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : DataContext<T>, IRepository<T> where T : BaseModelEntity,new()
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public Repository(ISqlSugarClient context) : base(context)//注意这里要有默认值等于null
        {
        }


        /// <summary>
        /// 添加返回实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> InsertReturnEntityAsync(T entity)
        {
            entity.Id =SnowFlakeSingle.instance.getID();
            return await Db.Insertable(entity).ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// 更新忽略空值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateIgnoreNullAsync(T entity)
        {
            return await Db.Updateable(entity).IgnoreColumns(true).ExecuteCommandAsync()>0;
        }


        /// <summary>
        /// 逻辑多删除
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteByLogicAsync(List<long> ids)
        {
            var entitys = await Db.Queryable<T>().Where(u => ids.Contains(u.Id)).ToListAsync();
            entitys.ForEach(u=>u.IsDeleted=true);
            return await Db.Updateable(entitys).ExecuteCommandAsync()>0;
        }


        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="storeName"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public async Task<List<S>> StoreAsync<S>(string storeName, object para)
        {
            return await Db.Ado.UseStoredProcedure().SqlQueryAsync<S>(storeName, para);
        }


        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="pars"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(QueryCondition pars)
        {
            return await QueryConditionHandler(pars).ToListAsync();
        }

        /// <summary>
        /// 仓储扩展方法:单表查询通用分页 
        /// </summary>
        /// <returns></returns>
        public  async Task<PageModel<List<T>>> CommonPageAsync(QueryPageCondition pars)
        {
            RefAsync<int> tolCount = 0;
            var result = await  QueryConditionHandler(new QueryCondition() {OrderBys=pars.OrderBys,Parameters=pars.Parameters } ).ToPageListAsync(pars.Index, pars.Size, tolCount);
            return new PageModel<List<T>>
            {
                Total = tolCount.Value,
                Data = result
            };
        }



        private ISugarQueryable<T> QueryConditionHandler(QueryCondition pars)
        {
            var sugarParamters = pars.Parameters.Select(it => (IConditionalModel)new ConditionalModel()
            {
                ConditionalType = it.Type,
                FieldName = it.Key,
                FieldValue = it.Value
            }).ToList();
            var query = Db.Queryable<T>();
            if (pars.OrderBys != null)
            {
                foreach (var item in pars.OrderBys)
                {
                    query.OrderBy(item.ToSqlFilter());
                }
            }
            return query.Where(sugarParamters);
        }

    }

   
}