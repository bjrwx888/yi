using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Enums;

namespace Yi.Framework.Ddd.Dtos
{
    public class PagedAndSortedResultRequestDto : IPagedAndSortedResultRequestDto
    {
        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
        public string? SortBy { get; set; }
        public OrderByEnum SortType { get; set; } = OrderByEnum.Desc;
        public List<IConditionalModel> Conditions { get; set; }
    }
}
