using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{

    public partial class RoleEntity
    {

        [Navigate(typeof(RoleMenuEntity),nameof(RoleMenuEntity.RoleId),nameof(RoleMenuEntity.MenuId))]
        public List<MenuEntity>? Menus { get; set; }

        [Navigate(typeof(RoleDeptEntity), nameof(RoleDeptEntity.RoleId), nameof(RoleDeptEntity.DeptId))]
        public List<DeptEntity>? Depts { get; set; }
    }
}
