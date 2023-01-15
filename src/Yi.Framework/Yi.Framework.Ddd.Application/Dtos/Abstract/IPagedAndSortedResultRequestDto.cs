using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Dtos
{
    public interface IPagedAndSortedResultRequestDto
    {
         int PageIndex { get; set; }
         int PageSize { get; set; }
         string? PageSort { get; set; }
    }
}
