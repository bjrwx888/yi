using System.Linq.Expressions;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Filters;

namespace Yi.Framework.Infrastructure.Sqlsugar.Filters
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
            _Db.QueryFilter.AddTableFilter(expression);
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
