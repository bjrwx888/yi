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
    public class SqlsugarDataFilter : IDataFilter
    {
        private ISqlSugarClient _Db { get; set; }
        public SqlsugarDataFilter(ISqlSugarClient sqlSugarClient)
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
            return this;
        }

        public IDisposable Enable<TFilter>() where TFilter : class
        {
            throw new NotImplementedException("暂时没有单独还原过滤器的方式");
        }

        public bool IsEnabled<TFilter>() where TFilter : class
        {
            throw new NotImplementedException("暂时没有判断过滤器的方式");
        }

        public void RemoveFilter<TFilter>() where TFilter : class
        {
            _Db.QueryFilter.Clear<TFilter>();
        }

        public void Dispose()
        {
            _Db.QueryFilter.Restore();
        }
    }
}
