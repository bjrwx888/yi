using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Filters;

namespace Yi.Framework.Core.Sqlsugar.Filters
{
    public class SqlsugarDataFiter : IDataFilter
    {
        private ISqlSugarClient _Db { get; set; }
        public SqlsugarDataFiter(ISqlSugarClient sqlSugarClient)
        {
            _Db = sqlSugarClient;
        }
        public void AddFilter<TFilter>(Expression<Func<TFilter, bool>> expression) where TFilter : class
        {
            _Db.QueryFilter.AddTableFilter<TFilter>(expression);
        }

        public IDisposable Disable<TFilter>() where TFilter : class
        {
            _Db.QueryFilter.ClearAndBackup<TFilter>();
            throw new NotImplementedException();
        }

        public IDisposable Enable<TFilter>() where TFilter : class
        {
            _Db.QueryFilter.Restore();
            throw new NotImplementedException();
        }

        public bool IsEnabled<TFilter>() where TFilter : class
        {
            throw new NotImplementedException();
        }

        public void RemoveFilter<TFilter>() where TFilter : class
        {
            _Db.QueryFilter.Clear<TFilter>();
            throw new NotImplementedException();
        }
    }
}
