using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Model.SeedData
{
    public  class MenuSeed: AbstractSeed<MenuEntity>
    {
        public override List<MenuEntity> GetSeed()
        {
            MenuEntity menu = new MenuEntity()
            {
               MenuName="首页",
               PermissionCode="*:*:*"
            };
            Entitys.Add(menu);
            return Entitys;
        }
    }
}
