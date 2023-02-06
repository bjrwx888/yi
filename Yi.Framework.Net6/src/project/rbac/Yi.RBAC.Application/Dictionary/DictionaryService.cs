using Yi.RBAC.Application.Contracts.Dictionary;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Dictionary.Dtos;
using Yi.RBAC.Domain.Dictionary.Entities;
using Yi.Framework.Ddd.Services;
using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Domain.Dictionary.Repositories;

namespace Yi.RBAC.Application.Dictionary
{
    /// <summary>
    /// Dictionary服务实现
    /// </summary>
    [AppService]
    public class DictionaryService : CrudAppService<DictionaryEntity, DictionaryGetOutputDto, DictionaryGetListOutputDto, long, DictionaryGetListInputVo, DictionaryCreateInputVo, DictionaryUpdateInputVo>,
       IDictionaryService, IAutoApiService
    {
        /// <summary>
        /// 查询
        /// </summary>
        [Autowired]
        private IDictionaryRepository _dictionaryRepository { get; set; }
        public override async Task<PagedResultDto<DictionaryGetListOutputDto>> GetListAsync(DictionaryGetListInputVo input)
        {
            int total = 0;
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
