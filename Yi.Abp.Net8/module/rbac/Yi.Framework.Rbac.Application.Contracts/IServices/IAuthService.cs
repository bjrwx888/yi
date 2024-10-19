using Volo.Abp.Application.Dtos;
using Yi.Framework.Rbac.Application.Contracts.Dtos.Account;

namespace Yi.Framework.Rbac.Application.Contracts.IServices;

public interface IAuthService
{
    Task<AuthOutputDto?> TryGetByOpenIdAsync(string openId, string authType);
    Task<AuthOutputDto> CreateAsync(AuthCreateOrUpdateInputDto input);
}