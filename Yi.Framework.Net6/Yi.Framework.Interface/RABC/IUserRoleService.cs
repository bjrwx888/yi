using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DtoModel.Base.Dto;
using Yi.Framework.Interface.Base;
using Yi.Framework.Model.RABC.Entitys;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface.RABC
{
    public partial interface IUserRoleService : IBaseService<UserRoleEntity>
    {


        Task<PageModel<List<UserEntity>>> GetAllocatedPageList(ParUserRoleDto role, PageParModel page);


        Task<PageModel<List<UserEntity>>> GetUnAllocatedPageList(ParUserRoleDto role, PageParModel page);

        /// <summary>
        /// 给角色设置用户，多角色，多用户
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<bool> GiveRoleSetUser(List<long> roleIds, List<long?> userIds);

        /// <summary>
        /// 给角色分配用户
        /// </summary>
        /// <param name="crRoleUserDto"></param>
        /// <returns></returns>
        Task<bool> SelectRoleUserAll(CrRoleUserDto crRoleUserDto);

        /// <summary>
        /// 批量取消用户授权角色
        /// </summary>
        /// <param name="crRoleUserDto"></param>
        /// <returns></returns>
        Task<bool> CancelRoleUserAll(CrRoleUserDto crRoleUserDto);
    }
}
