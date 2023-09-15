using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Module.DictionaryManager.Dtos.DictionaryType;
using Yi.Framework.Module.DictionaryManager.Entities;

namespace Yi.Framework.Module.DictionaryManager
{
    /// <summary>
    /// DictionaryType服务实现
    /// </summary>
    [ApiDescriptionSettings("DictionaryManager")]
    public class DictionaryTypeService : CrudAppService<DictionaryTypeEntity, DictionaryTypeGetOutputDto, DictionaryTypeGetListOutputDto, long, DictionaryTypeGetListInputVo, DictionaryTypeCreateInputVo, DictionaryTypeUpdateInputVo>,
       IDictionaryTypeService, IDynamicApiController, ITransient
    {


        public async override Task<PagedResultDto<DictionaryTypeGetListOutputDto>> GetListAsync(DictionaryTypeGetListInputVo input)
        {

            RefAsync<int> total = 0;
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
