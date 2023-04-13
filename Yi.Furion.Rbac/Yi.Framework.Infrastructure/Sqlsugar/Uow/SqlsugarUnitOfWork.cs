using Furion;
using Furion.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Uow;

namespace Yi.Framework.Infrastructure.Sqlsugar.Uow
{
    public class SqlsugarUnitOfWork : IUnitOfWork, ISingleton
    {
        public ISqlSugarClient Db { get; set; }
        public ITenant Tenant { get; set; }
        public bool IsTran { get; set; }
        public bool IsCommit { get; set; }
        public bool IsClose { get; set; }

        public void Dispose()
        {

            if (IsTran && IsCommit == false)
            {
                Tenant.RollbackTran();
            }
            if (Db.Ado.Transaction == null && IsClose == false)
            {
                Db.Close();
            }
        }

        public bool Commit()
        {
            if (IsTran && IsCommit == false)
            {
                Tenant.CommitTran();
                IsCommit = true;
            }
            if (Db.Ado.Transaction == null && IsClose == false)
            {
                Db.Close();
                IsClose = true;
            }
            return IsCommit;
        }

        public IRepository<T> GetRepository<T>()
        {
            return App.GetRequiredService<IRepository<T>>();
        }
    }
}
