using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
using Yi.Framework.Ddd.Entities;

namespace Yi.RBAC.Domain.Identity.Entities
{
    /// <summary>
    /// 用户角色关系表
    ///</summary>
    [SugarTable("UserRole")]
    public partial class UserRoleEntity : IEntity<long>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }
    }
}
