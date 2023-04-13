using System.Collections.Generic;
using Yi.Furion.Rbac.Core.Entities;

namespace Yi.Furion.Rbac.Core.Dtos
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
