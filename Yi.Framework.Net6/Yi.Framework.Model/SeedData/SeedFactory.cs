using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Model.SeedData
{
    public  class SeedFactory
    {
        public static List<UserEntity> GetUserSeed()
        { 
            return new UserSeed().GetSeed();
        }
        public static List<RoleEntity> GetRoleSeed()
        {
            return new RoleSeed().GetSeed();
        }
        public static List<MenuEntity> GetMenuSeed()
        {
            return new MenuSeed().GetSeed();
        }
        public static List<UserRoleEntity> GetUserRoleSeed(List<UserEntity> users,List<RoleEntity> roles)
        {
            return new List<UserRoleEntity>();
        }
    }
}
