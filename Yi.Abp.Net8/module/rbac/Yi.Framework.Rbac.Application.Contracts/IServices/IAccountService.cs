using Yi.Framework.Rbac.Domain.Shared.Dtos;

namespace Yi.Framework.Rbac.Application.Contracts.IServices
{
    public interface IAccountService
    {
        Task<UserRoleMenuDto> Get();
    }
}
