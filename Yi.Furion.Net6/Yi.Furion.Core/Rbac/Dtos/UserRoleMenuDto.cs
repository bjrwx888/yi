using System.Collections.Generic;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Core.Rbac.Dtos
{
    public class UserRoleMenuDto
    {
        public UserEntity User { get; set; } = new();
        public HashSet<RoleEntity> Roles { get; set; } = new();
        public HashSet<MenuEntity> Menus { get; set; } = new();

        public List<string> RoleCodes { get; set; } = new();
        public List<string> PermissionCodes { get; set; } = new();
    }
}
