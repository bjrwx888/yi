using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    public partial class UserEntity:BaseModelEntity
    {
        /// <summary>
        ///  看好啦！ORM精髓，导航属性
        ///</summary>
        [Navigate(typeof(UserRoleEntity), nameof(UserRoleEntity.UserId), nameof(UserRoleEntity.RoleId))]
        public List<RoleEntity> Roles { get; set; } 
    }
}
