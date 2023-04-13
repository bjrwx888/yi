using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Furion.Rbac.Core.Dtos;
using Yi.Furion.Rbac.Core.Entities;

namespace Yi.Furion.Rbac.Sqlsugar.Core.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        /// <summary>
        /// 获取当前登录用户的所有信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserRoleMenuDto> GetUserAllInfoAsync(long userId);

    }
}
