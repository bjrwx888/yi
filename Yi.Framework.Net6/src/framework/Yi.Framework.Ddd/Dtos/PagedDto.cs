using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Dtos
{
    public class PagedDto<T>
    {
        public PagedDto(long totalCount, List<T> items)
        {
            Total = totalCount;
            Items = items;
        }
        public long Total { get; set; }

        public List<T> Items{ get; set; }
}
}
