using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{

    public partial class RoleEntity
    {
        //[Navigate(typeof(UserRoleEntity), nameof(UserRoleEntity.RoleId), nameof(UserRoleEntity.UserId))]
        //public List<UserEntity> Users { get; set; }
    }
}
