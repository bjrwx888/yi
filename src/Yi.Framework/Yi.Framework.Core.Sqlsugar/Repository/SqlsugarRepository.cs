using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attribute;
using Yi.Framework.Ddd.Repository;

namespace Yi.Framework.Core.Sqlsugar.Repository
{
    [AppService]
    public class SqlsugarRepository<T> : SimpleClient<T>, IRepository<T> where T : class, new()
    {
        public SqlsugarRepository(ISqlSugarClient context) : base(context)
        {
        }
        protected ISugarQueryable<T> _DbQueryable { get { return base.Context.Queryable<T>(); } set { } }

        protected ISqlSugarClient _Db { get { return base.Context; } set { } }

    }
}
