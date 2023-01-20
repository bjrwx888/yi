using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Data.Filters
{
    public interface IDataFilter
    {
        IDisposable Enable<TFilter>() where TFilter :class;

        IDisposable Disable<TFilter>() where TFilter : class;

        bool IsEnabled<TFilter>() where TFilter : class;

        void AddFilter<TFilter>(Expression<Func<TFilter, bool>> expression) where TFilter : class;

        void RemoveFilter<TFilter>() where TFilter : class;
    }

}
