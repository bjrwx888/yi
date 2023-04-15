using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.Rbac.Dtos.Config;

namespace Yi.Furion.Application.Rbac.Services
{
    /// <summary>
    /// Config服务抽象
    /// </summary>
    public interface IConfigService : ICrudAppService<ConfigGetOutputDto, ConfigGetListOutputDto, long, ConfigGetListInputVo, ConfigCreateInputVo, ConfigUpdateInputVo>
    {

    }
}
