using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Model.SeedData
{
    public class UserSeed : AbstractSeed<UserEntity>
    {
        public override List<UserEntity> GetSeed()
        {
            UserEntity user = new UserEntity()
            {
                Name = "大橙子",
                UserName = "cc",
                Nick = "橙子",
                Password = "123456"
            };
            user.BuildPassword();
            Entitys.Add(user);
            return Entitys;
        }
    }
}
