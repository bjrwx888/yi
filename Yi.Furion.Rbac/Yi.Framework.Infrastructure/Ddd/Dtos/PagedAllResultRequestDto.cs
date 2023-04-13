using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Framework.Infrastructure.Ddd.Services.Abstract;

namespace Yi.Framework.Infrastructure.Ddd.Dtos
{
    public class PagedAllResultRequestDto : PagedAndSortedResultRequestDto, IPagedAllResultRequestDto, IPagedAndSortedResultRequestDto, IPageTimeResultRequestDto
    {
        /// <summary>
        /// 查询开始时间条件
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 查询结束时间条件
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
