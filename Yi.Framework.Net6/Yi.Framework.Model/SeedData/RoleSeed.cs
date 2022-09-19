using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Model.SeedData
{
    public  class RoleSeed : AbstractSeed<RoleEntity>
    {
        public override List<RoleEntity> GetSeed()
        {
            RoleEntity role = new RoleEntity()
            {
                RoleName = "管理员",
                RoleCode = "admin",
            };
            Entitys.Add(role);
            return Entitys;
        }
    }
}
