using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Furion.Core.Rbac.Dtos.Config;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Application.Rbac.Services.Impl
{
    /// <summary>
    /// Config服务实现
    /// </summary>
    [ApiDescriptionSettings("RBAC")]
    public class ConfigService : CrudAppService<ConfigEntity, ConfigGetOutputDto, ConfigGetListOutputDto, long, ConfigGetListInputVo, ConfigCreateInputVo, ConfigUpdateInputVo>,
       IConfigService,IDynamicApiController,ITransient
    {
        /// <summary>
        /// 多查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<ConfigGetListOutputDto>> GetListAsync(ConfigGetListInputVo input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.ConfigKey), x => x.ConfigKey.Contains(input.ConfigKey!))
                          .WhereIF(!string.IsNullOrEmpty(input.ConfigName), x => x.ConfigName!.Contains(input.ConfigName!))
                          .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<ConfigGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }
    }
}
