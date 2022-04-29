using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IRoleService
    {
        /// <summary>
        /// DbTest
        /// </summary>
        /// <returns></returns>
        Task<List<RoleEntity>> DbTest();

        /// <summary>
        /// 通过角色id获取角色实体包含菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<RoleEntity> GetInMenuByRoleId(long roleId);

        /// <summary>
        /// 给角色设置菜单，多角色，多菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        Task<bool> GiveRoleSetMenu(List<long> roleIds, List<long> menuIds);
    }
}
