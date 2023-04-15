using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Application.Rbac.Dtos.User;

namespace Yi.Furion.Application.Rbac.Services
{
    /// <summary>
    /// User服务抽象
    /// </summary>
    public interface IUserService : ICrudAppService<UserGetOutputDto, UserGetListOutputDto, long, UserGetListInputVo, UserCreateInputVo, UserUpdateInputVo>
    {
    }
}
