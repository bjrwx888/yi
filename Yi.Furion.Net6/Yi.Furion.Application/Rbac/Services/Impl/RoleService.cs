using Furion.DatabaseAccessor;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Application.Rbac.Domain;
using Yi.Furion.Core.Rbac.Dtos.Role;
using Yi.Furion.Core.Rbac.Entities;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Application.Rbac.Services.Impl
{
    /// <summary>
    /// Role服务实现
    /// </summary>
    public class RoleService : CrudAppService<RoleEntity, RoleGetOutputDto, RoleGetListOutputDto, long, RoleGetListInputVo, RoleCreateInputVo, RoleUpdateInputVo>,
       IRoleService, ITransient, IDynamicApiController
    {
        public RoleService(RoleManager roleManager, IRepository<RoleDeptEntity> roleDeptRepository) =>
           (_roleManager, _roleDeptRepository) =
            (roleManager, roleDeptRepository);
        private RoleManager _roleManager { get; set; }

        private IRepository<RoleDeptEntity> _roleDeptRepository;

        [UnitOfWork]
        public async Task UpdateDataScpoceAsync(UpdateDataScpoceInput input)
        {
            //只有自定义的需要特殊处理
            if (input.DataScope == DataScopeEnum.CUSTOM)
            {
                await _roleDeptRepository.DeleteAsync(x => x.RoleId == input.RoleId);
                var insertEntities = input.DeptIds.Select(x => new RoleDeptEntity {Id=SnowflakeHelper.NextId, DeptId = x, RoleId = input.RoleId }).ToList();
                await _roleDeptRepository.InsertRangeAsync(insertEntities);
            }
            await _repository._Db.Updateable(new RoleEntity() { Id = input.RoleId, DataScope = input.DataScope }).UpdateColumns(x => x.DataScope).ExecuteCommandAsync();

        }

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
        [UnitOfWork]
        public override async Task<RoleGetOutputDto> CreateAsync(RoleCreateInputVo input)
        {
            RoleGetOutputDto outputDto;
            //using (var uow = _unitOfWorkManager.CreateContext())
            //{
            var entity = await MapToEntityAsync(input);
            await _repository.InsertAsync(entity);
            outputDto = await MapToGetOutputDtoAsync(entity);
            await _roleManager.GiveRoleSetMenuAsync(new List<long> { entity.Id }, input.MenuIds);
            //    uow.Commit();
            //}

            return outputDto;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public override async Task<RoleGetOutputDto> UpdateAsync(long id, RoleUpdateInputVo input)
        {
            var dto = new RoleGetOutputDto();
            //using (var uow = _unitOfWorkManager.CreateContext())
            //{
            var entity = await _repository.GetByIdAsync(id);
            await MapToEntityAsync(input, entity);
            await _repository.UpdateAsync(entity);

            await _roleManager.GiveRoleSetMenuAsync(new List<long> { id }, input.MenuIds);

            dto = await MapToGetOutputDtoAsync(entity);
            //    uow.Commit();
            //}
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
