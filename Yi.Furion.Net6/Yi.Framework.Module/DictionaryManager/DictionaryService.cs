using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Module.DictionaryManager.Dtos.Dictionary;
using Yi.Framework.Module.DictionaryManager.Entities;


namespace Yi.Framework.Module.DictionaryManager
{
    /// <summary>
    /// Dictionary服务实现
    /// </summary>
    [ApiDescriptionSettings("DictionaryManager")]
    public class DictionaryService : CrudAppService<DictionaryEntity, DictionaryGetOutputDto, DictionaryGetListOutputDto, long, DictionaryGetListInputVo, DictionaryCreateInputVo, DictionaryUpdateInputVo>,
       IDictionaryService,IDynamicApiController,ITransient
    {
        /// <summary>
        /// 查询
        /// </summary>

        public override async Task<PagedResultDto<DictionaryGetListOutputDto>> GetListAsync(DictionaryGetListInputVo input)
        {
            RefAsync<int> total = 0;
            var entities = await _DbQueryable.WhereIF(input.DictType is not null, x => x.DictType == input.DictType)
                      .WhereIF(input.DictLabel is not null, x => x.DictLabel!.Contains(input.DictLabel!))
                      .WhereIF(input.State is not null, x => x.State == input.State)
                      .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<DictionaryGetListOutputDto>
            {
                Total = total,
                Items = await MapToGetListOutputDtosAsync(entities)
            };
        }


        /// <summary>
        /// 根据字典类型获取字典列表
        /// </summary>
        /// <param name="dicType"></param>
        /// <returns></returns>
        [Route("/api/dictionary/dic-type/{dicType}")]
        public async Task<List<DictionaryGetListOutputDto>> GetDicType([FromRoute] string dicType)
        {
            var entities = await _repository.GetListAsync(u => u.DictType == dicType && u.State == true);
            var result = await MapToGetListOutputDtosAsync(entities);
            return result;
        }
    }
}
