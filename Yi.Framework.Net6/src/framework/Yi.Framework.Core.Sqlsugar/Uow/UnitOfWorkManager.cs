using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Uow;

namespace Yi.Framework.Core.Sqlsugar.Uow
{
    /// <summary>
    /// 此部分为sqlsugr的魔改版本
    /// </summary>
    internal class UnitOfWorkManager : IUnitOfWorkManager
    {
        public UnitOfWorkManager(ISqlSugarClient db)
        {
            this.Db = db;
        }
        public ISqlSugarClient Db { get; set; }
        public IUnitOfWork CreateContext(bool isTran = true)
        {
            UnitOfWork uow = new UnitOfWork();
            return CreateContext(isTran, uow);
        }
        private IUnitOfWork CreateContext(bool isTran, UnitOfWork sugarUnitOf)
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
