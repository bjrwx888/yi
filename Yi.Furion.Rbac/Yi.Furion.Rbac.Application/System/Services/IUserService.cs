using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Rbac.Application.System.Dtos.User;

namespace Yi.Furion.Rbac.Application.System.Services
{
    /// <summary>
    /// User服务抽象
    /// </summary>
    public interface IUserService : ICrudAppService<UserGetOutputDto, UserGetListOutputDto, long, UserGetListInputVo, UserCreateInputVo, UserUpdateInputVo>
    {
    }
}
