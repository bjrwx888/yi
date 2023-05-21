using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.Sqlsugar;

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
            //这里Aop进行数据权限过滤
            var userId = _currentUser.Id;
            if (userId == 0) return;
            _logger.LogInformation($"用户【{userId}】访问Aop");

            /*
             * 这里数据权限，步骤：
             * 1：获取用户id
             * 2：通过用户id，获取该用户的全部角色
             * 3：便利每一个角色获取全部的数据权限
             * 4：会涉及部门表的筛选，所以还需要获取用户的所在部门，如果没有部门，那就是过滤到只看自己
             * 5：可直接使用DB进行查询部门即可
             */
        }
    }
}
