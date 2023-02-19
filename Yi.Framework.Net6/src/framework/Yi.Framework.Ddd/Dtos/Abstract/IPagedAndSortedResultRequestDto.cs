using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Enums;

namespace Yi.Framework.Ddd.Dtos
{
    public interface IPagedAndSortedResultRequestDto
    {
         int PageNum { get; set; }
         int PageSize { get; set; }
         string? SortBy { get; set; }

        OrderByEnum SortType { get; set; }
    }
}
