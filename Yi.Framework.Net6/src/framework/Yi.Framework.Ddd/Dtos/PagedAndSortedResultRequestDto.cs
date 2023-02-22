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
        /// <summary>
        /// 查询当前页条件
        /// </summary>
        public int PageNum { get; set; } = 1;

        /// <summary>
        /// 查询分页大小条件
        /// </summary>
        public int PageSize { get; set; } = int.MaxValue;

        /// <summary>
        /// 查询排序字段条件
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// 查询排序类别条件
        /// </summary>
        public OrderByEnum SortType { get; set; } = OrderByEnum.Desc;
    }
}
