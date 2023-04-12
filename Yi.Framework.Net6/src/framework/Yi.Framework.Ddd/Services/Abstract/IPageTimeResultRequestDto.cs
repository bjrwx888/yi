using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.Framework.Ddd.Services.Abstract
{
    public interface IPageTimeResultRequestDto: IPagedAndSortedResultRequestDto
    {
         DateTime? StartTime { get; set; }
         DateTime? EndTime { get; set; }
    }
}
