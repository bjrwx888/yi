using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Services.Abstract
{
    public interface IUpdateAppService<TEntityDto, in TKey>
        : IUpdateAppService<TEntityDto, TKey, TEntityDto>
    {

    }

    public interface IUpdateAppService<TGetOutputDto, in TKey, in TUpdateInput>
        : IApplicationService
    {
        Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input);
    }

}
