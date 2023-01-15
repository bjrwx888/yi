using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Dtos
{
    public class PagedAndSortedResultRequestDto : IPagedAndSortedResultRequestDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? PageSort { get; set; }

    }
}
