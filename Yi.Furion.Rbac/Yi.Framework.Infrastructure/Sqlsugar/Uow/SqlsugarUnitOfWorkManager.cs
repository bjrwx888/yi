using Furion.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Uow;

namespace Yi.Framework.Infrastructure.Sqlsugar.Uow
{
    /// <summary>
    /// 此部分为sqlsugr的魔改版本
    /// </summary>
    internal class SqlsugarUnitOfWorkManager : IUnitOfWorkManager,ISingleton
    {
        public SqlsugarUnitOfWorkManager(ISqlSugarClient db)
        {
            Db = db;
        }
        public ISqlSugarClient Db { get; set; }
        public IUnitOfWork CreateContext(bool isTran = true)
        {
            SqlsugarUnitOfWork uow = new SqlsugarUnitOfWork();
            return CreateContext(isTran, uow);
        }
        private IUnitOfWork CreateContext(bool isTran, SqlsugarUnitOfWork sugarUnitOf)
        {
            sugarUnitOf.Db = Db;
            sugarUnitOf.Tenant = Db.AsTenant();
            sugarUnitOf.IsTran = isTran;
            Db.Open();
            if (isTran)
                Db.AsTenant().BeginTran();
            return sugarUnitOf;
        }
    }
}
