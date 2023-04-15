using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Application.Rbac.Domain
{
    public class UserManager : ITransient
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IRepository<UserRoleEntity> _repositoryUserRole;
        private readonly IRepository<UserPostEntity> _repositoryUserPost;
        public UserManager(IRepository<UserEntity> repository, IRepository<UserRoleEntity> repositoryUserRole, IRepository<UserPostEntity> repositoryUserPost) =>
            (_repository, _repositoryUserRole, _repositoryUserPost) =
            (repository, repositoryUserRole, repositoryUserPost);

        /// <summary>
        /// 给用户设置角色
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task GiveUserSetRoleAsync(List<long> userIds, List<long> roleIds)
        {
            //删除用户之前所有的用户角色关系（物理删除，没有恢复的必要）
            await _repositoryUserRole.DeleteAsync(u => userIds.Contains(u.UserId));

            if (roleIds is not null)
            {
                //遍历用户
                foreach (var userId in userIds)
                {
                    //添加新的关系
                    List<UserRoleEntity> userRoleEntities = new();

                    foreach (var roleId in roleIds)
                    {
                        userRoleEntities.Add(new UserRoleEntity() { Id = SnowflakeHelper.NextId, UserId = userId, RoleId = roleId });
                    }
                    //一次性批量添加
                    await _repositoryUserRole.InsertRangeAsync(userRoleEntities);
                }
            }
        }


        /// <summary>
        /// 给用户设置岗位
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="postIds"></param>
        /// <returns></returns>
        public async Task GiveUserSetPostAsync(List<long> userIds, List<long> postIds)
        {
            //删除用户之前所有的用户角色关系（物理删除，没有恢复的必要）
            await _repositoryUserPost.DeleteAsync(u => userIds.Contains(u.UserId));
            if (postIds is not null)
            {
                //遍历用户
                foreach (var userId in userIds)
                {
                    //添加新的关系
                    List<UserPostEntity> userPostEntities = new();
                    foreach (var post in postIds)
                    {
                        userPostEntities.Add(new UserPostEntity() { Id = SnowflakeHelper.NextId, UserId = userId, PostId = post });
                    }

                    //一次性批量添加
                    await _repositoryUserPost.InsertRangeAsync(userPostEntities);
                }

            }
        }

    }

}