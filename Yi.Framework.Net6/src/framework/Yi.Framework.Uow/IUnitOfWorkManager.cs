//namespace Yi.Framework.Uow
//{
//    public interface IUnitOfWorkManager
//    {
//        SugarUnitOfWork CreateContext(bool isTran = true);
//    }


//    /// <summary>
//    /// 这个放到实现类中
//    /// </summary>
//    public class SugarUnitOfWorkManager : IUnitOfWorkManager
//    {
//        public SugarUnitOfWorkManager(ISqlSugarClient db)
//        {
//            this.Db = db;
//        }
//        public SugarUnitOfWork Db { get; set; }
//        public SugarUnitOfWork CreateContext(bool isTran = true)
//        {
//            return Db.CreateContext(isTran);
//        }
//    }


//    /// <summary>
//    /// 下面这个定死了
//    /// </summary>
//    public class SugarUnitOfWork : IDisposable
//    {
//        public ISqlSugarClient Db { get; internal set; }
//        public ITenant Tenant { get; internal set; }
//        public bool IsTran { get; internal set; }
//        public bool IsCommit { get; internal set; }
//        public bool IsClose { get; internal set; }

//        public void Dispose()
//        {

//            if (this.IsTran && IsCommit == false)
//            {
//                this.Tenant.RollbackTran();
//            }
//            if (this.Db.Ado.Transaction == null && IsClose == false)
//            {
//                this.Db.Close();
//            }
//        }

//        public SimpleClient<T> GetRepository<T>() where T : class, new()
//        {
//            TenantAttribute tenantAttribute = typeof(T).GetCustomAttribute<TenantAttribute>();
//            if (tenantAttribute == null)
//            {
//                return new SimpleClient<T>(Db);
//            }
//            else
//            {
//                return new SimpleClient<T>(Db.AsTenant().GetConnection(tenantAttribute.configId));
//            }
//        }

//        public RepositoryType GetMyRepository<RepositoryType>() where RepositoryType : ISugarRepository, new()
//        {
//            var result = new RepositoryType();
//            var type = typeof(RepositoryType).GetGenericArguments()[0];
//            TenantAttribute tenantAttribute = type.GetCustomAttribute<TenantAttribute>();
//            if (tenantAttribute == null)
//            {
//                result.Context = this.Db;
//            }
//            else
//            {
//                result.Context = this.Db.AsTenant().GetConnection(tenantAttribute.configId);
//            }
//            return result;
//        }

//        public bool Commit()
//        {
//            if (this.IsTran && this.IsCommit == false)
//            {
//                this.Tenant.CommitTran();
//                IsCommit = true;
//            }
//            if (this.Db.Ado.Transaction == null && this.IsClose == false)
//            {
//                this.Db.Close();
//                IsClose = true;
//            }
//            return IsCommit;
//        }
//    }
//}