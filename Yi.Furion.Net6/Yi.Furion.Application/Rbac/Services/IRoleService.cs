using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.Rbac.Dtos.Role;

namespace Yi.Furion.Application.Rbac.Services
{
    /// <summary>
    /// Role服务抽象
    /// </summary>
    public interface IRoleService : ICrudAppService<RoleGetOutputDto, RoleGetListOutputDto, long, RoleGetListInputVo, RoleCreateInputVo, RoleUpdateInputVo>
    {

    }
}
