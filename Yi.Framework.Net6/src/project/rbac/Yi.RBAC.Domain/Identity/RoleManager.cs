using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Entities;

namespace Yi.RBAC.Domain.Identity
{
    [AppService]
    public class RoleManager
    {
        private IRepository<RoleEntity> _repository;
        private IRepository<RoleMenuEntity> _roleMenuRepository;
        public RoleManager(IRepository<RoleEntity> repository, IRepository<RoleMenuEntity> roleMenuRepository)
        {
            _repository = repository;
            _roleMenuRepository = roleMenuRepository;
        }

        /// <summary>
        /// 给角色设置菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public async Task GiveRoleSetMenuAsync(List<long> roleIds, List<long> menuIds)
        {
            //这个是需要事务的，在service中进行工作单元
            await _roleMenuRepository.DeleteAsync(u => roleIds.Contains(u.RoleId));
            //遍历用户
            foreach (var roleId in roleIds)
            {
                //添加新的关系
                List<RoleMenuEntity> roleMenuEntity = new();
                foreach (var menu in menuIds)
                {
                    roleMenuEntity.Add(new RoleMenuEntity() {Id=SnowflakeHelper.NextId, RoleId = roleId, MenuId = menu });
                }
                //一次性批量添加
                await _roleMenuRepository.InsertRangeAsync(roleMenuEntity);
            }

        }
    }
}
