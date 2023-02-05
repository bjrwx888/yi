using Yi.RBAC.Application.Contracts.Dictionary;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Dictionary.Dtos;
using Yi.RBAC.Domain.Dictionary.Entities;
using Yi.Framework.Ddd.Services;
using Yi.RBAC.Domain.Dictionary.Repositories;
using Yi.Framework.Ddd.Dtos;

namespace Yi.RBAC.Application.Dictionary
{
    /// <summary>
    /// DictionaryType服务实现
    /// </summary>
    [AppService]
    public class DictionaryTypeService : CrudAppService<DictionaryTypeEntity, DictionaryTypeGetOutputDto, DictionaryTypeGetListOutputDto, long, DictionaryTypeGetListInputVo, DictionaryTypeCreateInputVo, DictionaryTypeUpdateInputVo>,
       IDictionaryTypeService, IAutoApiService
    {

        [Autowired]
        private IDictionaryTypeRepository _dictionaryTypeRepository { get; set; }

        public async override Task<PagedResultDto<DictionaryTypeGetListOutputDto>> GetListAsync(DictionaryTypeGetListInputVo input)
        {
            var data = await _dictionaryTypeRepository.SelectGetListAsync(await MapToEntityAsync(input), input);
            return new PagedResultDto<DictionaryTypeGetListOutputDto>
            {
                Total = data.Total,
                Items = await MapToGetListOutputDtosAsync(data.Items)
            };
        }

    }
}
