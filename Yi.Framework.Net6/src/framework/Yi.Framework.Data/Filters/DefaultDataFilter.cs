using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Data.Filters
{
    public class DefaultDataFilter : IDataFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultDataFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        public IDisposable Disable<TFilter>() where TFilter : class
        {
            return this;
        }

        public IDisposable Enable<TFilter>() where TFilter : class
        {
            return this;
        }

        public bool IsEnabled<TFilter>() where TFilter : class
        {
            return false;
        }

        public void RemoveFilter<TFilter>() where TFilter : class
        {
        }
        public void RemoveAndBackup<TFilter>() where TFilter : class
        {
        }

        public void AddFilter<TFilter>(Expression<Func<TFilter, bool>> expression) where TFilter : class
        {
        }

        public void Dispose()
        {
  
        }
    }
}
