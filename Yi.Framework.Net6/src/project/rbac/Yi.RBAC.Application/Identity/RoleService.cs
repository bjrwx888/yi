using Yi.RBAC.Application.Contracts.Identity;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.Framework.Ddd.Services;
using Yi.RBAC.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Yi.Framework.Uow;

namespace Yi.RBAC.Application.Identity
{
    /// <summary>
    /// Role服务实现
    /// </summary>
    [AppService]
    public class RoleService : CrudAppService<RoleEntity, RoleGetOutputDto, RoleGetListOutputDto, long, RoleGetListInputVo, RoleCreateInputVo, RoleUpdateInputVo>,
       IRoleService, IAutoApiService
    {
        [Autowired]
        private RoleManager _roleManager { get; set; }

        [Autowired]
        private IUnitOfWorkManager _unitOfWorkManager { get; set; }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<RoleGetOutputDto> CreateAsync(RoleCreateInputVo input)
        {
            RoleGetOutputDto outputDto;
            using (var uow = _unitOfWorkManager.CreateContext())
            {
                var entity = await MapToEntityAsync(input);
                await _repository.InsertAsync(entity);
                outputDto = await MapToGetOutputDtoAsync(entity);
                await _roleManager.GiveRoleSetMenuAsync(new List<long> { entity.Id }, input.MenuIds);
                uow.Commit();
            }

            return outputDto;
        }
    }
}
