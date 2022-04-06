using SqlSugar;
using System.Data;
using System.Linq.Expressions;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Query;

/***这里面写的代码不会给覆盖,如果要重新生成请删除 Repository.cs ***/
namespace Yi.Framework.Repository
{
    /// <summary>
    /// 仓储模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : DataContext<T>, IRepository<T> where T : class, new()
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
            return await Db.Insertable(entity).ExecuteReturnEntityAsync();
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
        /// 仓储扩展方法:单表查询通用分页 
        /// </summary>
        /// <returns></returns>
        public  async Task<PageModel<List<T>>> CommonPage(QueryCondition pars)
        {
            RefAsync<int> tolCount = 0;
            var sugarParamters = pars.Parameters.Select(it => (IConditionalModel)new ConditionalModel()
            {
                ConditionalType = it.ConditionalType,
                FieldName = it.FieldName,
                FieldValue = it.FieldValue
            }).ToList();
            var query = Db.Queryable<T>();
            if (pars.OrderBys != null)
            {
                foreach (var item in pars.OrderBys)
                {
                    query.OrderBy(item.ToSqlFilter());
                }
            }
            var result =await query.Where(sugarParamters).ToPageListAsync(pars.Index, pars.Size, tolCount);
          
            return new PageModel<List<T>>
            {
                Total = tolCount.Value,
                Data = result
            };
        }
    }

   
}