using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class RoleService 
    {
        public async Task<List<RoleEntity>> DbTest()
        {
            return await _repository._Db.Queryable<RoleEntity>().ToListAsync();
        }
        public async Task<bool> GiveRoleSetMenu(List<long> roleIds, List<long> menuIds)
        {
          var _repositoryRoleMenu=  _repository.ChangeRepository<Repository<RoleMenuEntity>>();
            //多次操作，需要事务确保原子性
            return await _repositoryRoleMenu.UseTranAsync(async () =>
            {

                //遍历用户
                foreach (var roleId in roleIds)
                {
                    //删除用户之前所有的用户角色关系（物理删除，没有恢复的必要）
                    await _repositoryRoleMenu.DeleteAsync(u => u.RoleId==roleId);

                    //添加新的关系
                    List<RoleMenuEntity> roleMenuEntity = new();
                    foreach (var menu in menuIds)
                    {
                        roleMenuEntity.Add(new RoleMenuEntity() { RoleId = roleId,MenuId=menu });
                    }

                    //一次性批量添加
                    await _repositoryRoleMenu.InsertReturnSnowflakeIdAsync(roleMenuEntity);
                }
            });


        }

        public async Task<RoleEntity> GetInMenuByRoleId(long roleId)
        {
            return await _repository._Db.Queryable<RoleEntity>().Includes(u => u.Menus).InSingleAsync(roleId);
        
        }



        public async Task<PageModel<List<RoleEntity>>> SelctPageList(RoleEntity role, PageParModel page)
        {
            RefAsync<int> total = 0;
            var data = await _repository._Db.Queryable<RoleEntity>()
                    .WhereIF(!string.IsNullOrEmpty(role.RoleName), u => u.RoleName.Contains(role.RoleName))
                     .WhereIF(!string.IsNullOrEmpty(role.RoleCode), u => u.RoleCode.Contains(role.RoleCode))
                    .WhereIF(page.StartTime.IsNotNull() && page.EndTime.IsNotNull(), u => u.CreateTime >= page.StartTime && u.CreateTime <= page.EndTime)
                     .Where(u => u.IsDeleted == role.IsDeleted)
                    .OrderBy(u => u.OrderNum, OrderByType.Desc)
                    .ToPageListAsync(page.PageNum, page.PageSize, total);

            return new PageModel<List<RoleEntity>>(data, total);
        }
    }
}
