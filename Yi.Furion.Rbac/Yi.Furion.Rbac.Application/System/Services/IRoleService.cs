using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Rbac.Application.System.Dtos.Role;

namespace Yi.Furion.Rbac.Application.System.Services
{
    /// <summary>
    /// Role服务抽象
    /// </summary>
    public interface IRoleService : ICrudAppService<RoleGetOutputDto, RoleGetListOutputDto, long, RoleGetListInputVo, RoleCreateInputVo, RoleUpdateInputVo>
    {

    }
}
