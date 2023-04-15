using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Framework.Infrastructure.Ddd.Services.Abstract
{
    public interface IPageTimeResultRequestDto : IPagedAndSortedResultRequestDto
    {
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
    }
}
