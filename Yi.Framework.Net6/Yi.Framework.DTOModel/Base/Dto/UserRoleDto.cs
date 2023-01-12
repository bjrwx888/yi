using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.RABC.Entitys;

namespace Yi.Framework.DtoModel.Base.Dto
{
    public class UserRoleDto
    {
        public UserEntity? User { get; set; }
        public List<RoleEntity>? Roles { get; set; }
    }

    public class UpUserRoleDto
    {
        public long userId { get; set; }
        public List<long> roleIds { get; set; } = new();
    }

    public class ParUserRoleDto
    {
        public long roleId { get; set; }

        public string? userName { get; set; }

        public string? phone { get; set; }
    }

    public class CrRoleUserDto
    {
        public long roleId { get; set; }

        public List<long?> userIds { get; set; }
    }
}
