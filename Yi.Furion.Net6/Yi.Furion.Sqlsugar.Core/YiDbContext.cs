using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Furion.LinqBuilder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.Data.Filters;
using Yi.Framework.Infrastructure.Sqlsugar;
using Yi.Furion.Core.Rbac.Consts;
using Yi.Furion.Core.Rbac.Entities;
using Yi.Furion.Core.Rbac.Enums;
using Yi.Furion.Sqlsugar.Core.Repositories;

namespace Yi.Furion.Sqlsugar.Core
{
    /// <summary>
    /// 使用自定义上下文
    /// </summary>
    public class YiDbContext : SqlSugarDbContext
    {
        public YiDbContext(IOptions<DbConnOptions> options, ICurrentUser currentUser, ILogger<SqlSugarDbContext> logger) : base(options, currentUser, logger)
        {
        }


        //进行Aop数据权限过滤
        protected override void OnSqlSugarClientConfig(ISqlSugarClient sqlSugarClient)
        {
            //由于此处数据过滤为最底层，不能依赖仓储
            DataScopeFilter(sqlSugarClient);
        }

        /// <summary>
        /// 数据权限过滤
        /// </summary>
        private async void DataScopeFilter(ISqlSugarClient sqlSugarClient)
        {
            //这里Aop进行数据权限过滤
            var userId = _currentUser.Id;
            var userName = _currentUser.UserName;
            var deptId = _currentUser.DeptId;
            //超管或者
            if (userId == 0 || UserConst.Admin.Equals(userName)) return;

            //如果没有部门，只能看到自己
            if (deptId == 0)
            {
                sqlSugarClient.QueryFilter.AddTableFilter<UserEntity>(x => x.Id == userId);
                return;
            }
            /*
             * 这里数据权限，步骤：
             * 1：获取用户id
             * 2：通过用户id，获取该用户的全部角色
             * 3：便利每一个角色获取全部的数据权限
             * 4：会涉及部门表的筛选，所以还需要获取用户的所在部门，如果没有部门，那就是过滤到只看自己
             * 5：可直接使用DB进行查询部门即可
             */
            var roles = await sqlSugarClient.Queryable<RoleEntity>().Where(x => SqlFunc.Subqueryable<UserRoleEntity>().Where(ur =>ur.UserId == userId).Any()).ToListAsync();
            //获取到全部角色

            Expression<Func<UserEntity, bool>> expression = (x) => true;

            //添加数据过滤
            foreach (var role in roles.OrderBy(x => x.DataScope))
            {
                switch (role.DataScope)
                {
                    //全部数据权限，直接返回
                    case DataScopeEnum.ALL:
                        return;


                    //自定义数据过滤
                    case DataScopeEnum.CUSTOM:
                        var deptIds1 = (await  sqlSugarClient.Queryable<DeptEntity>().Where(x => SqlFunc.Subqueryable<RoleDeptEntity>().Where(ur => ur.RoleId == role.Id).Any()).ToListAsync()).Select(x => x.Id).ToList();
                        expression.Or(x => deptIds1.Contains(x.DeptId ?? -1));
                        break;


                    //部门数据过滤
                    case DataScopeEnum.DEPT:
                        expression.Or(x => x.DeptId == deptId);
                        break;


                    //部门及一下数据过滤
                    case DataScopeEnum.DEPT_FOLLOW:
                        var deptIds = ( await sqlSugarClient.Queryable<DeptEntity>().ToChildListAsync(x=>x.ParentId,deptId)).Select(x=>x.Id).ToList();
                        expression.Or(x => deptIds.Contains(x.DeptId ?? -1));
                        break;


                    //自己数据过滤
                    case DataScopeEnum.USER:
                        expression.Or(x => x.Id == userId);
                        break;
                    default:
                        break;
                }
                sqlSugarClient.QueryFilter.AddTableFilter<UserEntity>(expression);
            }
        }
    }
}
