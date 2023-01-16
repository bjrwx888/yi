using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Dtos.Abstract
{
    public interface IPagedAllResultRequestDto
    {
         DateTime? StartTime { get; set; }
         DateTime? EndTime { get; set; }
    }
}
