using SqlSugar;

namespace Yi.Framework.Repository
{
    public class DataContext<T> : SimpleClient<T> where T : class, new()
    {
        public DataContext(ISqlSugarClient context = null!) : base(context)
        {
            if (context == null)
            {
                base.Context = Db;
            }
        }
        /// <summary>
        /// SqlSugarScope操作数据库是线程安的可以单例
        /// </summary>
        public static SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
        {
            DbType = SqlSugar.DbType.MySql,
            //ConnectionString = Appsettings.app("ConnectionStrings","mysqlConnection") ,
            IsAutoCloseConnection = true
        },
            db =>
            {

                db.Aop.DataExecuting = (oldValue, entityInfo) =>
                {
                    //var httpcontext = ServiceLocator.Instance.GetService<IHttpContextAccessor>().HttpContext;
                    switch (entityInfo.OperationType)
                    {
                        case DataFilterType.InsertByObject:
                            if (entityInfo.PropertyName == "CreateUser")
                            {
                                //entityInfo.SetValue(new Guid(httpcontext.Request.Headers["Id"].ToString()));
                            }

                            if (entityInfo.PropertyName == "TenantId")
                            {
                                //现在不能直接给了，要根据判断一下租户等级，如果租户等级是1，不给，需要自己去赋值，如果租户等级是0，就执行下面的。
                                //entityInfo.SetValue(new Guid(httpcontext.Request.Headers["TenantId"].ToString()));
                                //查询的时候，也需要判断一下，如果是租户等级，不要租户条件，如果是超级租户，就返回所有
                            }
                            break;
                        case DataFilterType.UpdateByObject:
                            if (entityInfo.PropertyName == "ModifyTime")
                            {
                                entityInfo.SetValue(DateTime.Now);
                            }
                            if (entityInfo.PropertyName == "ModifyUser")
                            {
                                //entityInfo.SetValue(new Guid(httpcontext.Request.Headers["Id"].ToString()));
                            }
                            break;
                    }
                    //inset生效

                };
                //如果用单例配置要统一写在这儿
                db.Aop.OnLogExecuting = (s, p) =>
                {

                    Console.WriteLine("_______________________________________________");
                    Console.WriteLine(s);
                };

            });


    }
}
