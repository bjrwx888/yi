using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Ddd.Services.Abstract
{
    public interface ICreateAppService<TEntityDto>
        : ICreateAppService<TEntityDto, TEntityDto>
    {

    }

    public interface ICreateAppService<TGetOutputDto, in TCreateInput>
        : IApplicationService
    {
        Task<TGetOutputDto> CreateAsync(TCreateInput input);
    }
}
