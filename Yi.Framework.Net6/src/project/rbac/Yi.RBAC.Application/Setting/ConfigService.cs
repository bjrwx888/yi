using Yi.RBAC.Application.Contracts.Setting;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Setting.Dtos;
using Yi.RBAC.Domain.Setting.Entities;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Ddd.Dtos;
using SqlSugar;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Yi.RBAC.Application.Setting
{
    /// <summary>
    /// Config服务实现
    /// </summary>
    [AppService]

    public class ConfigService : CrudAppService<ConfigEntity, ConfigGetOutputDto, ConfigGetListOutputDto, long, ConfigGetListInputVo, ConfigCreateInputVo, ConfigUpdateInputVo>,
       IConfigService, IAutoApiService
    {
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
