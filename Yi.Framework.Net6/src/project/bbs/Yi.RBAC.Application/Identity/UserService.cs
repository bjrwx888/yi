using Yi.RBAC.Application.Contracts.Identity;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.Framework.Ddd.Services;

namespace Yi.RBAC.Application.Identity
{
    /// <summary>
    /// User服务实现
    /// </summary>
    [AppService]
    public class UserService : CrudAppService<UserEntity, UserGetOutputDto, UserGetListOutputDto, long, UserGetListInputVo, UserCreateInputVo, UserUpdateInputVo>,
       IUserService, IAutoApiService
    {
    }
}
