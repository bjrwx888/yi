using Yi.RBAC.Application.Contracts.Identity;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Ddd.Dtos;
using SqlSugar;

namespace Yi.RBAC.Application.Identity
{
    /// <summary>
    /// Dept服务实现
    /// </summary>
    [AppService]
    public class DeptService : CrudAppService<DeptEntity, DeptGetOutputDto, DeptGetListOutputDto, long, DeptGetListInputVo, DeptCreateInputVo, DeptUpdateInputVo>,
       IDeptService, IAutoApiService
    {
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
