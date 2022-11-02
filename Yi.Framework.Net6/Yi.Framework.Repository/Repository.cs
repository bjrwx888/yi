using SqlSugar;
using System.Data;
using System.Linq.Expressions;
using Yi.Framework.Common.Attribute;
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
    [AppService]
    public class Repository<T> : SimpleClient<T>, IRepository<T> where T : class, new()
    {
       
        public ISugarQueryable<T> _DbQueryable { get { return base.Context.Queryable<T>(); } set { } }

        public ISqlSugarClient _Db { get { return base.Context; } set { } }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public Repository(ISqlSugarClient context) : base(context)//注意这里要有默认值等于null
        {
            //开始Saas分库！
            //单个公共基础库+多个租户业务库
            
            //1:先判断操作对应实体上是否存在租户特性，如果存在说明是公共库

            //如果是公共库，直接使用默认库即可
            //如果不是公共库

            //2:根据上下文对象获取用户的租户id
            //3:根据租户id获取到对应上下文对象
            //4：替换仓储中的上下文对象即可

            //强化：如果租户要做到动态配置不写死，租户信息连接字符串等存入数据库，带入token中，还需要在sqlsugarAop中动态获取进行切换
            //base.Context =  context.AsTenant().GetConnectionScopeWithAttr<T>();

        }

        /// <summary>
        /// 异步事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<bool> UseTranAsync(Func<Task> func)
        {
            var con = Context;
            var res = await _Db.AsTenant().UseTranAsync(func);
            return res.IsSuccess;

        }

        /// <summary>
        /// 执行查询sql返回dto列表
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<S>> UseSqlAsync<S>(string sql, object parameters = null)
        {
            return await _Db.Ado.SqlQueryAsync<S>(sql, parameters);
        }

        /// <summary>
        /// 执行增删改sql返回状态
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> UseSqlAsync(string sql, object parameters)
        {
            return await _Db.Ado.ExecuteCommandAsync(sql,  parameters) >0;
        }

        ///// <summary>
        ///// 添加返回实体
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public async Task<T> InsertReturnEntityAsync(T entity)
        //{
        //    entity.Id =SnowFlakeSingle.instance.getID();
        //    return await _Db.Insertable(entity).ExecuteReturnEntityAsync();
        //}

        /// <summary>
        /// 更新忽略空值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateIgnoreNullAsync(T entity)
        {
            return await _Db.Updateable(entity).IgnoreColumns(true).ExecuteCommandAsync()>0;
        }


        ///// <summary>
        ///// 逻辑多删除
        ///// </summary>
        ///// <returns></returns>
        //public async Task<bool> DeleteByLogicAsync(List<long> ids)
        //{
        //    var entitys = await _Db.Queryable<T>().Where(u => ids.Contains(u.Id)).ToListAsync();
        //    entitys.ForEach(u=>u.IsDeleted=true);
        //    return await _Db.Updateable(entitys).ExecuteCommandAsync()>0;
        //}


        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="storeName"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public async Task<List<S>> StoreAsync<S>(string storeName, object para)
        {
            return await _Db.Ado.UseStoredProcedure().SqlQueryAsync<S>(storeName, para);
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



        public ISugarQueryable<T> QueryConditionHandler(QueryCondition pars)
        {
            var sugarParamters = pars.Parameters.Select(it => (IConditionalModel)new ConditionalModel()
            {
                ConditionalType = it.Type,
                FieldName = it.Key,
                FieldValue = it.Value
            }).ToList();
            var query = _Db.Queryable<T>();
            if (pars.OrderBys != null)
            {
                foreach (var item in pars.OrderBys)
                {
                    if (pars.IsAsc)
                    {
                        query.OrderBy(item.ToSqlFilter() + " asc");
                    }
                    else
                    {
                        query.OrderBy(item.ToSqlFilter() + " desc");
                    }
                }
            }
            return query.Where(sugarParamters);
        }


        /// <summary>
        /// 更新高级保存，验证重复
        /// </summary>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public async Task<bool> UpdateSuperSaveAsync(T data, Expression<Func<T, object>> columns)
        {
            var x = _Db.Storageable(data)
                           .SplitError(it => it.Any())
                           .SplitUpdate(it => true)
                           .WhereColumns(columns)//这里用name作为数据库查找条件
                           .ToStorage();
            return await x.AsInsertable.ExecuteCommandAsync() > 0;//插入可插入部分
        }

        /// <summary>
        /// 添加高级保存，验证重复
        /// </summary>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public async Task<bool> InsertSuperSaveAsync(T data, Expression<Func<T, object>> columns)
        {
            var x = _Db.Storageable(data)
                           .SplitError(it => it.Any())
                           .SplitInsert(it => true)
                           .WhereColumns(columns)//这里用name作为数据库查找条件
                           .ToStorage();
            return await x.AsInsertable.ExecuteCommandAsync() > 0;//插入可插入部分
        }
        /// <summary>
        /// 方法重载，多条件获取第一个值
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, OrderByType orderByType = OrderByType.Desc)
        {
            return await _Db.Queryable<T>().Where(where).OrderBy(order, orderByType).FirstAsync();
        }

        /// <summary>
        /// 方法重载，多条件获取范围
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, OrderByType orderByType = OrderByType.Desc)
        {
            return await _Db.Queryable<T>().Where(where).OrderBy(order, orderByType).ToListAsync();
        }
    }

   
}