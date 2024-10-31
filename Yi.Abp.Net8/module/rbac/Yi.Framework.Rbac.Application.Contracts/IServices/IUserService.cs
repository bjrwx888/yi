using System.Collections.Specialized;
using Yi.Framework.Ddd.Application.Contracts;
using Yi.Framework.Rbac.Application.Contracts.Dtos.User;

namespace Yi.Framework.Rbac.Application.Contracts.IServices
{
    /// <summary>
    /// User服务抽象
    /// </summary>
    public interface IUserService : IYiCrudAppService<UserGetOutputDto, UserGetListOutputDto, Guid, UserGetListInputVo, UserCreateInputVo, UserUpdateInputVo>
    {
        /// <summary>
        /// 装配返回数据中的创建用户和修改用户
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList">返回数据列表</param>
        public Task AssemblyBackUser<T>(List<T> dataList) where T : class, IBackUser;

        /// <summary>
        /// 装配用户，用来处理没有实现IBackUser接口，但需要返回用户信息
        /// 例如 AuditUserId列 对应 AuditUserNikeName这种
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="keyValues">key为原始id列，value为填充列</param>
        public Task AssemblyBackUser<T>(List<T> dataList, NameValueCollection keyValues) where T : class;
    }
}
