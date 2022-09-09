using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DTOModel;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IUserService
    {
        /// <summary>
        /// 测试仓储的上下文对象
        /// </summary>
        /// <returns></returns>
        Task<List<UserEntity>> DbTest();

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="userAction"></param>
        /// <returns></returns>
        Task<bool> Login(string userName, string password, Action<UserEntity> userAction = null);

        /// <summary>
        /// 注册方法
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userAction"></param>
        /// <returns></returns>
        Task<bool> Register(UserEntity userEntity, Action<UserEntity> userAction = null);

        /// <summary>
        /// 导航属性关联角色
        /// </summary>
        /// <returns></returns>
        Task<List<UserEntity>> GetListInRole();

        /// <summary>
        /// 给用户设置角色，多用户，多角色
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<bool> GiveUserSetRole(List<long> userIds, List<long> roleIds);

        /// <summary>
        /// 判断用户名是否存在，如果存在可返回该用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userAction"></param>
        /// <returns></returns>
        Task<bool> Exist(string userName, Action<UserEntity> userAction = null);

        /// <summary>
        /// 通过用户id得到角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<RoleEntity>> GetRoleListByUserId(long userId);

        /// <summary>
        /// 获取当前登录用户的所有信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserRoleMenuDto> GetUserAllInfo(long userId);

        /// <summary>
        /// 判断用户密码是否和原密码相同
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool JudgePassword(UserEntity user, string password);

        /// <summary>
        /// 动态条件分页查询
        /// </summary>
        /// <param name="user"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PageModel<List<UserEntity>>> SelctPageList(UserEntity user, PageParModel page);

        /// <summary>
        /// 菜单构建前端路由
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        List<VueRouterModel> RouterBuild(List<MenuEntity> menus);
    }
}
