using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.Framework.Ddd.Dtos.Abstract
{
    public interface IPagedAllResultRequestDto: IPageTimeResultRequestDto, IPagedAndSortedResultRequestDto
    {
         DateTime? StartTime { get; set; }
         DateTime? EndTime { get; set; }
    }
}
