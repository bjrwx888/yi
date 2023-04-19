using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Furion.Application.Rbac.Services;
using Yi.Furion.Core.Rbac.Dtos.Menu;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Application.Rbac.Services.Impl
{
    /// <summary>
    /// Menu服务实现
    /// </summary>
    public class MenuService : CrudAppService<MenuEntity, MenuGetOutputDto, MenuGetListOutputDto, long, MenuGetListInputVo, MenuCreateInputVo, MenuUpdateInputVo>,
       IMenuService, ITransient, IDynamicApiController
    {

        public override async Task<PagedResultDto<MenuGetListOutputDto>> GetListAsync(MenuGetListInputVo input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.MenuName), x => x.MenuName.Contains(input.MenuName!))
                        .WhereIF(input.State is not null, x => x.State == input.State)
                        .OrderByDescending(x => x.OrderNum)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<MenuGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }

        /// <summary>
        /// 查询当前角色的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<MenuGetListOutputDto>> GetListRoleIdAsync(long roleId)
        {
            var entities = await _DbQueryable.Where(m => SqlFunc.Subqueryable<RoleMenuEntity>().Where(rm => rm.RoleId == roleId && rm.MenuId == m.Id).Any()).ToListAsync();

            return await MapToGetListOutputDtosAsync(entities);
        }
    }
}