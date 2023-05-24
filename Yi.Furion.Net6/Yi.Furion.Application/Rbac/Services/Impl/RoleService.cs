using System.Linq.Expressions;
using System.Reflection.Metadata;
using Furion.DatabaseAccessor;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Application.Rbac.Domain;
using Yi.Furion.Core.Rbac.Dtos.Role;
using Yi.Furion.Core.Rbac.Dtos.User;
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
        public RoleService(RoleManager roleManager, IRepository<RoleDeptEntity> roleDeptRepository, IRepository<UserRoleEntity> userRoleRepository) =>
           (_roleManager, _roleDeptRepository, _userRoleRepository) =
            (roleManager, roleDeptRepository, userRoleRepository);
        private RoleManager _roleManager { get; set; }

        private IRepository<RoleDeptEntity> _roleDeptRepository;

        private IRepository<UserRoleEntity> _userRoleRepository;
        [UnitOfWork]
        public async Task UpdateDataScpoceAsync(UpdateDataScpoceInput input)
        {
            //只有自定义的需要特殊处理
            if (input.DataScope == DataScopeEnum.CUSTOM)
            {
                await _roleDeptRepository.DeleteAsync(x => x.RoleId == input.RoleId);
                var insertEntities = input.DeptIds.Select(x => new RoleDeptEntity { Id = SnowflakeHelper.NextId, DeptId = x, RoleId = input.RoleId }).ToList();
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


        /// <summary>
        /// 获取角色下的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="input"></param>
        /// <param name="isAllocated">是否在该角色下</param>
        /// <returns></returns>
        [Route("/api/role/auth-user/{roleId}/{isAllocated}")]
        public async Task<PagedResultDto<UserGetListOutputDto>> GetAuthUserByRoleIdAsync([FromRoute] long roleId, [FromRoute] bool isAllocated, [FromQuery] RoleAuthUserGetListInput input)
        {
            PagedResultDto<UserGetListOutputDto> output;
            //角色下已授权用户
            if (isAllocated == true)
            {
                output = await GetAllocatedAuthUserByRoleIdAsync(roleId, input);
            }
            //角色下未授权用户
            else
            {
                output = await GetNotAllocatedAuthUserByRoleIdAsync(roleId, input);
            }
            return output;
        }

        private async Task<PagedResultDto<UserGetListOutputDto>> GetAllocatedAuthUserByRoleIdAsync(long roleId, RoleAuthUserGetListInput input)
        {
            RefAsync<int> total = 0;
            var output = await _userRoleRepository._DbQueryable
                         .LeftJoin<UserEntity>((ur, u) => ur.UserId == u.Id && ur.RoleId == roleId)
                           .Where((ur, u) => ur.RoleId == roleId)
                         .WhereIF(!string.IsNullOrEmpty(input.UserName), (ur, u) => u.UserName.Contains(input.UserName))
                         .WhereIF(input.Phone is not null, (ur, u) => u.Phone.ToString().Contains(input.Phone.ToString()))
                         .Select((ur, u) => new UserGetListOutputDto { Id = u.Id }, true)
                         .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<UserGetListOutputDto>(total, output);
        }

        private async Task<PagedResultDto<UserGetListOutputDto>> GetNotAllocatedAuthUserByRoleIdAsync(long roleId, RoleAuthUserGetListInput input)
        {
            RefAsync<int> total = 0;
            var entities = await _userRoleRepository._Db.Queryable<UserEntity>()
                   .Where(u => SqlFunc.Subqueryable<UserRoleEntity>().Where(x => x.RoleId == roleId).Where(x => x.UserId == u.Id).NotAny())
                      .WhereIF(!string.IsNullOrEmpty(input.UserName), u => u.UserName.Contains(input.UserName))
                            .WhereIF(input.Phone is not null, u => u.Phone.ToString().Contains(input.Phone.ToString()))
                   .ToPageListAsync(input.PageNum, input.PageSize, total);
            var output = entities.Adapt<List<UserGetListOutputDto>>();
            return new PagedResultDto<UserGetListOutputDto>(total, output);
        }


        /// <summary>
        /// 批量给用户授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateAuthUserAsync(RoleAuthUserCreateOrDeleteInput input)
        {
            var userRoleEntities = input.UserIds.Select(u => new UserRoleEntity { Id = SnowflakeHelper.NextId, RoleId = input.RoleId, UserId = u }).ToList();
            await _userRoleRepository.InsertRangeAsync(userRoleEntities);
        }


        /// <summary>
        /// 批量取消授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteAuthUserAsync(RoleAuthUserCreateOrDeleteInput input)
        {
            await _userRoleRepository._Db.Deleteable<UserRoleEntity>().Where(x => x.RoleId == input.RoleId)
                .Where(x => input.UserIds.Contains(x.UserId))
                .ExecuteCommandAsync(); ;
        }
    }
}
