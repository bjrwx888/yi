using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Furion.Core.Rbac.Dtos;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Sqlsugar.Core.Repositories
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
