using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Ddd.Entities;

namespace Yi.Framework.Ddd.Services.Abstract
{
    public interface IReadOnlyAppService<TEntityDto, in TKey>
        : IReadOnlyAppService<TEntityDto, TEntityDto, TKey, PagedAndSortedResultRequestDto>
    {

    }

    public interface IReadOnlyAppService<TEntityDto, in TKey, in TGetListInput>
        : IReadOnlyAppService<TEntityDto, TEntityDto, TKey, TGetListInput>
    {

    }

    public interface IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput>
        : IApplicationService
    {
        Task<TGetOutputDto> GetAsync(TKey id);

        Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input);
    }
}
