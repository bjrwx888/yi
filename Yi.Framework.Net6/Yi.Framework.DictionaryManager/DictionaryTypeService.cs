using Cike.AutoWebApi.Setting;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Ddd.Dtos;
using SqlSugar;
using Yi.Framework.DictionaryManager.Dtos.DictionaryType;
using Yi.Framework.DictionaryManager.Entities;
using Yi.Framework.Core.Attributes;

namespace Yi.Framework.DictionaryManager
{
    /// <summary>
    /// DictionaryType服务实现
    /// </summary>
    [AppService]
    public class DictionaryTypeService : CrudAppService<DictionaryTypeEntity, DictionaryTypeGetOutputDto, DictionaryTypeGetListOutputDto, long, DictionaryTypeGetListInputVo, DictionaryTypeCreateInputVo, DictionaryTypeUpdateInputVo>,
       IDictionaryTypeService, IAutoApiService
    {


        public async override Task<PagedResultDto<DictionaryTypeGetListOutputDto>> GetListAsync(DictionaryTypeGetListInputVo input)
        {

            int total = 0;
            var entities = await _DbQueryable.WhereIF(input.DictName is not null, x => x.DictName.Contains(input.DictName!))
                      .WhereIF(input.DictType is not null, x => x.DictType!.Contains(input.DictType!))
                      .WhereIF(input.State is not null, x => x.State == input.State)
                      .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                      .ToPageListAsync(input.PageNum, input.PageSize, total);

            return new PagedResultDto<DictionaryTypeGetListOutputDto>
            {
                Total = total,
                Items = await MapToGetListOutputDtosAsync(entities)
            };
        }

    }
}
