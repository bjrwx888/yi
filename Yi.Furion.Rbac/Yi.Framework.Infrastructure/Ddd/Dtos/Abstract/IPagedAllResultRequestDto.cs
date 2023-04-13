using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Services.Abstract;

namespace Yi.Framework.Infrastructure.Ddd.Dtos.Abstract
{
    public interface IPagedAllResultRequestDto : IPageTimeResultRequestDto, IPagedAndSortedResultRequestDto
    {
    }
}
