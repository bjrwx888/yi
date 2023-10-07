using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Furion.Application.App.Services;
using Yi.Furion.Core.App.Dtos.Trends;
using Yi.Furion.Core.App.Entities;

namespace Yi.Furion.Application.App.Services.Impl
{
    /// <summary>
    /// Trends服务实现
    /// </summary>
    [ApiDescriptionSettings("App")]
    public class TrendsService : CrudAppService<TrendsEntity, TrendsGetOutputDto, TrendsGetListOutputDto, long, TrendsGetListInput, TrendsCreateInput, TrendsUpdateInputVo>,
       ITrendsService, IDynamicApiController, ITransient
    {
        /// <summary>
        /// 多查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<TrendsGetListOutputDto>> GetListAsync(TrendsGetListInput input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;

            var entities = await _DbQueryable
                          .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<TrendsGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }
    }
}
