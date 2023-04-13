using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Ddd.Dtos.Abstract
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Items { get; set; }
    }
}
