using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos.Abstract;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.Framework.Ddd.Dtos
{
    public class PagedAllResultRequestDto : PagedAndSortedResultRequestDto, IPageTimeResultRequestDto, IPagedAndSortedResultRequestDto
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
