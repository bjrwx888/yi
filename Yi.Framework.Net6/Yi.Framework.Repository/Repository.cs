using SqlSugar;
using System.Data;
using System.Linq.Expressions;
using static Yi.Framework.Repository.QueryParametersExtensions;

/***这里面写的代码不会给覆盖,如果要重新生成请删除 Repository.cs ***/
namespace Yi.Framework.Repository
{
    /// <summary>
    /// 仓储模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : DataContext<T> ,IRepository<T> where T : class,  new()
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

        

        public async Task<T> InsertReturnEntityAsync(T entity)
        {
            return await Db.Insertable(entity).ExecuteReturnEntityAsync();
        }
        /// <summary>
        /// whereif与where混搭，多租户
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="whereBool"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, bool whereBool, Expression<Func<T, bool>> where, bool isTenant = true)
        {
            return await  Db.Queryable<T>().WhereIF(whereBool, where).Where(whereExpression).WhereTenant(isTenant).ToListAsync();
        }

        /// <summary>
        /// where重载，多租户
        /// </summary>
        /// <param name="whereBool"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(bool whereBool, Expression<Func<T, bool>> where, bool isTenant = true)
        {
            return await Db.Queryable<T>().WhereIF(whereBool, where).WhereTenant(isTenant). ToListAsync();
        }


      
        /// <summary>
        /// 左连接，三表连接，返回最右边的列表,多租户
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="joinQueryable1"></param>
        /// <param name="joinQueryable12"></param>
        /// <param name="whereLambda"></param>
        /// <param name="selectLambda"></param>
        /// <returns></returns>
        public async Task<List<R>> LeftJoinListAsync<M, R>(Expression<Func<T, M, bool>> joinQueryable1, Expression<Func<T, M, R, bool>> joinQueryable12, Expression<Func<T, bool>> whereLambda, Expression<Func<T, M, R>> selectLambda, bool isTenant = true)
        {
            return await Db.Queryable<T>().LeftJoin<M>(joinQueryable1)
            .LeftJoin<R>(joinQueryable12)
            .Where(whereLambda)
            .WhereTenant(isTenant)
            .Select(selectLambda)
            .ToListAsync();
        }

        public async Task<List<S>> StoreAsync<S>(string storeName, object para)
        {
            return await Db.Ado.UseStoredProcedure().SqlQueryAsync<S>(storeName, para);
        }

        /// <summary>
        /// 调用sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public async Task<DataTable> SqlDataTableAsync(string sql, object para = null)
        {
            return await Db.Ado.GetDataTableAsync(sql, para);
        }

        /// <summary>
        /// 导航属性mapper返回一个，多租户
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<T> FirstMapperAsync<T2, TT>(Expression<Func<TT, ManyToMany>> expression,bool isTenant=true)
        {
            return await Db.Queryable<T>().Mapper<T, T2, TT>(expression).WhereTenant(isTenant).FirstAsync();
        }

        /// <summary>
        /// 导航属性mapper返回一组，多租户
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<T>> ToListMapperAsync<T2, TT>(Expression<Func<TT, ManyToMany>> expression, bool isTenant = true)
        {
            return await Db.Queryable<T>() .Mapper<T, T2, TT>(expression).WhereTenant(isTenant).ToListAsync();
        }




        /// <summary>
        /// 导航属性mapper返回一组.同时添加条件，多租户
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="expression"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<List<T>> ToListMapperAsync<T2, TT>(Expression<Func<TT, ManyToMany>> expression, bool whereBool, Expression<Func<T, bool>> where, bool isTenant = true)
        {
            return await Db.Queryable<T>().Mapper<T, T2, TT>(expression).WhereIF(whereBool,where).WhereTenant(isTenant).ToListAsync();
        }


        /// <summary>
        /// 仓储扩展方法:单表查询通用分页 
        /// </summary>
        /// <returns></returns>
        public object CommonPage(QueryParameters pars, int pageIndex, int pageSize)
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
            var result = query.Where(sugarParamters).ToPageList(pageIndex, pageSize, ref tolCount);
            return new
            {
                count = tolCount,
                data = result
            };
        }


        /// <summary>
        /// 额外添加动态条件拼接
        /// </summary>
        /// <returns></returns>
        public object CommonPage(QueryParameters pars, int pageIndex, int pageSize, bool whereBool, Expression<Func<T, bool>> where)
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
            var result = query.WhereIF(whereBool, where).Where(sugarParamters).ToPageList(pageIndex, pageSize, ref tolCount);
            return new
            {
                count = tolCount,
                data = result
            };
        }



        /// <summary>
        /// 导航属性mapper分页多条件
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public object CommonPageMapper<T2, TT>(Expression<Func<TT, ManyToMany>> expression, QueryParameters pars, int pageIndex, int pageSize,bool whereBool, Expression<Func<T, bool>> where)
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
            var result = query.Mapper < T, T2, TT>(expression).WhereIF(whereBool, where). Where(sugarParamters).ToPageList(pageIndex, pageSize, ref tolCount);
            return new
            {
                count = tolCount,
                data = result
            };
        }

    }

    /// <summary>
    /// 通用查询参数
    /// </summary>
    public class QueryParameters
    {
        public List<QueryParameter> Parameters { get; set; } = new List<QueryParameter>();
        public List<string> OrderBys { get; set; } = new List<string>();
    }

    public static class QueryParametersExtensions
    {

        public static ISugarQueryable<T,M,R> WhereTenant<T, M, R>(this ISugarQueryable<T, M, R> db, bool isTenant = true)
        {
            if (isTenant)
            {
                var sugarParamters = new QueryParameters().SetParameters(new Dictionary<string, string>()).Parameters.Select(it => (IConditionalModel)new ConditionalModel()
                {
                    ConditionalType = it.ConditionalType,
                    FieldName = it.FieldName,
                    FieldValue = it.FieldValue
                }).ToList();
                return db.Where(sugarParamters);
            }


            return db;

        }
        public static ISugarQueryable<T> WhereTenant<T>(this ISugarQueryable<T> db, bool isTenant = true)
        {
            if (isTenant)
            {
                var sugarParamters = new QueryParameters().SetParameters(new Dictionary<string, string>()).Parameters.Select(it => (IConditionalModel)new ConditionalModel()
                {
                    ConditionalType = it.ConditionalType,
                    FieldName = it.FieldName,
                    FieldValue = it.FieldValue
                }).ToList();
              return  db.Where(sugarParamters);
            }

            return db;


        }

        public static QueryParameters SetParameters(this QueryParameters queryParameters, Dictionary<string, string> dic,bool IsTenant=true)
        {
            //var httpcontext = ServiceLocator.Instance.GetService<IHttpContextAccessor>().HttpContext;
            queryParameters.OrderBys = new List<string> { "CreateTime" };
         

            foreach (var p in dic)
            {
                QueryParameter qp = null;
                if (p.Key == "IsDeleted" || p.Key=="Id")
                {
                    qp=  new QueryParameter() { FieldName = p.Key, FieldValue = p.Value, ConditionalType = ConditionalType.Equal };
          
                }
                else
                {
                    qp= new QueryParameter() { FieldName = p.Key, FieldValue = p.Value };
               
                }
                queryParameters.Parameters.Add(qp);
            }
            if (IsTenant)
            {
                //if (httpcontext.Request.Headers["TenantLevel"].ToString() == "0")
                //{
                //    queryParameters.Parameters.Add(new QueryParameter() { ConditionalType = ConditionalType.Equal, FieldName = "TenantId", FieldValue = httpcontext.Request.Headers["TenantId"].ToString() });
                //}
            }
           
            return queryParameters;
        }

        /// <summary>
        /// 通用查询参数
        /// </summary>
        public class QueryParameter
        {
            public string FieldName { get; set; }
            public string FieldValue { get; set; }
            public ConditionalType ConditionalType { get; set; } = ConditionalType.Like;

        }
    }
}