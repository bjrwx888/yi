using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Furion.Core.Rbac.Dtos.Dept;
using Yi.Furion.Core.Rbac.Entities;
using Yi.Furion.Sqlsugar.Core.Repositories;

namespace Yi.Furion.Application.Rbac.Services.Impl
{
    /// <summary>
    /// Dept服务实现
    /// </summary>
    [ApiDescriptionSettings("RBAC")]
    public class DeptService : CrudAppService<DeptEntity, DeptGetOutputDto, DeptGetListOutputDto, long, DeptGetListInputVo, DeptCreateInputVo, DeptUpdateInputVo>,
       IDeptService, ITransient, IDynamicApiController
    {
        private IDeptRepository _deptRepository;
        public DeptService(IDeptRepository deptRepository) { _deptRepository = deptRepository; }
        [NonAction]
        public async Task<List<long>> GetChildListAsync(long deptId)
        {
            return await _deptRepository.GetChildListAsync(deptId);
        }

        /// <summary>
        /// 通过角色id查询该角色全部部门
        /// </summary>
        /// <returns></returns>
        //[Route("{roleId}")]
        public async Task<List<DeptGetListOutputDto>> GetRoleIdAsync([FromRoute] long roleId)
        {
            var entities = await _deptRepository.GetListRoleIdAsync(roleId);
            return await MapToGetListOutputDtosAsync(entities);
        }

        /// <summary>
        /// 多查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<DeptGetListOutputDto>> GetListAsync(DeptGetListInputVo input)
        {
            RefAsync<int> total = 0;
            var entities = await _DbQueryable
                           .WhereIF(!string.IsNullOrEmpty(input.DeptName), u => u.DeptName.Contains(input.DeptName!))
                            .WhereIF(input.State is not null, u => u.State == input.State)
                           .OrderBy(u => u.OrderNum, OrderByType.Asc)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<DeptGetListOutputDto>
            {
                Items = await MapToGetListOutputDtosAsync(entities),
                Total = total
            };
        }
    }
}
