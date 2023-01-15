using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Dtos
{
    public class PagedResultDto<T>:ListResultDto<T>, IPagedResult<T>
    {
        public long Total { get; set; } 

        public PagedResultDto()
        {

        }

        public PagedResultDto(long totalCount, IReadOnlyList<T> items)
            : base(items)
        {
            Total = totalCount;
        }
    }
}
