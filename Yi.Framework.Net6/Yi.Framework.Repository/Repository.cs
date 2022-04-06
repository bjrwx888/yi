using SqlSugar;
using System.Data;
using System.Linq.Expressions;
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
        public Repository(ISqlSugarClient context = null) : base(context)//注意这里要有默认值等于null
        {
            if (context == null)
            {
                base.Context = Db;
            }
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
        public object CommonPage(QueryCondition pars)
        {
            int tolCount = 0;
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
                    query.OrderBy(item.ToSqlFilter());//格式 id asc或者 id desc
                }
            }
            var result = query.Where(sugarParamters).ToPageList(pars.Index, pars.Size, ref tolCount);
            return new
            {
                count = tolCount,
                data = result
            };
        }
    }

   
}