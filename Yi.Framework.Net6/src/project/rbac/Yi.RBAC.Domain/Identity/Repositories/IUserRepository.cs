using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Shared.Identity.Dtos;

namespace Yi.RBAC.Domain.Identity.Repositories
{
    public interface IUserRepository:IRepository<UserEntity>
    {
        /// <summary>
        /// 获取当前登录用户的所有信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserRoleMenuDto> GetUserAllInfoAsync(long userId);
    }
}
