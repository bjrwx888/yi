using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Dtos
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Items { get; set; }
    }
}
