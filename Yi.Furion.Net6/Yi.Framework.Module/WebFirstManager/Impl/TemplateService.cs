using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Module.WebFirstManager.Dtos.Template;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Impl
{
    [ApiDescriptionSettings("WebFirstManager")]
    public class TemplateService : CrudAppService<TemplateEntity, TemplateDto, long, TemplateGetListInput>, ITemplateService, IDynamicApiController, ITransient
    {
        public async override Task<PagedResultDto<TemplateDto>> GetListAsync([FromQuery] TemplateGetListInput input)
        {
            RefAsync<int> total = 0;
            var entities = await _DbQueryable.WhereIF(input.Name is not null, x => x.Name.Equals(input.Name!))
                      .ToPageListAsync(input.PageNum, input.PageSize, total);

            return new PagedResultDto<TemplateDto>
            {
                Total = total,
                Items = await MapToGetListOutputDtosAsync(entities)
            };
        }
    }
}
