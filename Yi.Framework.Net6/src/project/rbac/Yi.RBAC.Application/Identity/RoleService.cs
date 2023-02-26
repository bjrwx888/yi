using Yi.RBAC.Application.Contracts.Identity;
using Cike.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.Framework.Ddd.Services;
using Yi.RBAC.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Yi.Framework.Uow;
using Yi.Framework.Ddd.Dtos;
using SqlSugar;
using Microsoft.AspNetCore.Mvc;

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


        public override async Task<PagedResultDto<RoleGetListOutputDto>> GetListAsync(RoleGetListInputVo input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.RoleCode), x => x.RoleCode.Contains(input.RoleCode!))
                .WhereIF(!string.IsNullOrEmpty(input.RoleName), x => x.RoleName.Contains(input.RoleName!))
                        .WhereIF(input.State is not null, x => x.State == input.State)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<RoleGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }

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

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<RoleGetOutputDto> UpdateAsync(long id, RoleUpdateInputVo input)
        {
            var dto = new RoleGetOutputDto();
            using (var uow = _unitOfWorkManager.CreateContext())
            {
                var entity = await _repository.GetByIdAsync(id);
                await MapToEntityAsync(input, entity);
                await _repository.UpdateAsync(entity);

                await _roleManager.GiveRoleSetMenuAsync(new List<long> { id }, input.MenuIds);

                dto = await MapToGetOutputDtoAsync(entity);
                uow.Commit();
            }
            return dto;
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [Route("/api/role/{id}/{state}")]
        public async Task<RoleGetOutputDto> UpdateStateAsync([FromRoute] long id, [FromRoute] bool state)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
            {
                throw new ApplicationException("角色未存在");
            }

            entity.State = state;
            await _repository.UpdateAsync(entity);
            return await MapToGetOutputDtoAsync(entity);
        }
    }
}
