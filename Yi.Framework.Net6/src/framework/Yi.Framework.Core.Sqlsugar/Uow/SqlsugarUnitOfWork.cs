using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Model;
using Yi.Framework.Ddd.Repositories;
using Yi.Framework.Uow;

namespace Yi.Framework.Core.Sqlsugar.Uow
{
    public class SqlsugarUnitOfWork : IUnitOfWork
    {
        public ISqlSugarClient Db { get; set; }
        public ITenant Tenant { get; set; }
        public bool IsTran { get; set; }
        public bool IsCommit { get; set; }
        public bool IsClose { get; set; }

        public void Dispose()
        {

            if (this.IsTran && IsCommit == false)
            {
                this.Tenant.RollbackTran();
            }
            if (this.Db.Ado.Transaction == null && IsClose == false)
            {
                this.Db.Close();
            }
        }

        public bool Commit()
        {
            if (this.IsTran && this.IsCommit == false)
            {
                this.Tenant.CommitTran();
                IsCommit = true;
            }
            if (this.Db.Ado.Transaction == null && this.IsClose == false)
            {
                this.Db.Close();
                IsClose = true;
            }
            return IsCommit;
        }

        public IRepository<T> GetRepository<T>()
        {
            if (ServiceLocatorModel.Instance is null)
                throw new ArgumentNullException("ServiceLocatorModel.Instance");
            //又是你这个骚东西
            return ServiceLocatorModel.Instance.GetRequiredService<IRepository<T>>();
        }
    }
}
