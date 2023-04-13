using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Framework.Infrastructure.Ddd.Dtos
{
    public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
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
