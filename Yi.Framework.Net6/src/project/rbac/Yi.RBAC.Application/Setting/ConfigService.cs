using Yi.RBAC.Application.Contracts.Setting;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Setting.Dtos;
using Yi.RBAC.Domain.Setting.Entities;
using Yi.Framework.Ddd.Services;

namespace Yi.RBAC.Application.Setting
{
    /// <summary>
    /// Config服务实现
    /// </summary>
    [AppService]
    public class ConfigService : CrudAppService<ConfigEntity, ConfigGetOutputDto, ConfigGetListOutputDto, long, ConfigGetListInputVo, ConfigCreateInputVo, ConfigUpdateInputVo>,
       IConfigService, IAutoApiService
    {
    }
}
