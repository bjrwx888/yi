﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;

namespace Yi.Framework.DTOModel
{
    public class UserRoleMenuDto
    {
        public UserEntity User { get; set; }=new ();
        public HashSet<RoleEntity> Roles { get; set; } = new();
        public HashSet<MenuEntity> Menus { get; set; }=new();

        public List<string> RoleCodes { get; set; } =new();
        public List<string> PermissionCodes { get; set; } = new();
    }
}
