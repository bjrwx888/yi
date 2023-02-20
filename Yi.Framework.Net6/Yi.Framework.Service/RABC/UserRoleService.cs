using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.Base.Dto;
using Yi.Framework.Interface.RABC;
using Yi.Framework.Model.RABC.Entitys;
using Yi.Framework.Repository;
using Yi.Framework.Service.Base;

namespace Yi.Framework.Service.RABC
{
    public partial class UserRoleService : BaseService<UserRoleEntity>, IUserRoleService
    {
        public UserRoleService(IRepository<UserRoleEntity> repository) : base(repository)
        {
        }

        public async Task<PageModel<List<UserEntity>>> GetAllocatedPageList(ParUserRoleDto role, PageParModel page)
        {
            RefAsync<int> total = 0;
            var data = await _repository._Db.Queryable<UserRoleEntity>()
                .LeftJoin<UserEntity>((ur, u) => ur.UserId == u.Id)
                .Where(ur => ur.RoleId == role.roleId)
                .WhereIF(!string.IsNullOrEmpty(role.userName), (ur,u) => u.UserName.Contains(role.userName))
                .OrderBy((ur,u) => u.OrderNum, OrderByType.Desc)
                .Select((ur,u)=>u)
                .ToPageListAsync(page.PageNum, page.PageSize, total);
                

            return new PageModel<List<UserEntity>>(data, total);
        }


        public async Task<PageModel<List<UserEntity>>> GetUnAllocatedPageList(ParUserRoleDto role, PageParModel page)
        {
            RefAsync<int> total = 0;
            var data = await _repository._Db.Queryable<UserEntity>()
                .Where(u=>u.IsDeleted==false)
                .Where(u=>SqlFunc.Subqueryable<UserRoleEntity>().Where(ur=>ur.UserId==u.Id&&ur.RoleId==role.roleId).NotAny())
                .WhereIF(!string.IsNullOrEmpty(role.userName), u => u.UserName.Contains(role.userName))
                .OrderBy(u => u.OrderNum, OrderByType.Desc)
                .ToPageListAsync(page.PageNum, page.PageSize, total);


            return new PageModel<List<UserEntity>>(data, total);
        }


        public async Task<bool> GiveRoleSetUser(List<long> roleIds, List<long?> userIds)
        {
            //多次操作，需要事务确保原子性
            return await _repository.UseTranAsync(async () =>
            {
                //遍历角色
                foreach (var roleId in roleIds)
                {
                    //添加新的关系
                    List<UserRoleEntity> roleDeptEntity = new();
                    foreach (var user in userIds)
                    {
                        roleDeptEntity.Add(new UserRoleEntity() { RoleId = roleId, UserId = user });
                    }

                    //一次性批量添加
                    await _repository.InsertReturnSnowflakeIdAsync(roleDeptEntity);
                }
            });
        }


        public async Task<bool> SelectRoleUserAll(CrRoleUserDto crRoleUserDto)
        {
            var res1 = await GiveRoleSetUser(new List<long> { crRoleUserDto.roleId }, crRoleUserDto.userIds);
            return res1;
        }

        public async Task<bool> CancelRoleUserAll(CrRoleUserDto crRoleUserDto)
        {
            var res1 = await _repository.DeleteAsync(u => u.RoleId == crRoleUserDto.roleId && crRoleUserDto.userIds.Contains(u.UserId));
            return res1;
        }

    }
}
