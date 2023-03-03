using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.DataSeeds;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Shared.Identity.EnumClasses;

namespace Yi.RBAC.Domain.DataSeeds
{
    //支持依赖注入执行
    //[AppService(typeof(IDataSeed<>))]
    
    //支持启动时执行
    [AppService(typeof(IDataSeed))]
    public class UserDataSeed : AbstractDataSeed<UserEntity>
    {
        public UserDataSeed(IRepository<UserEntity> repository) : base(repository)
        {
        }

        public override List<UserEntity> GetSeedData()
        {
            var entities=new List<UserEntity>();
            UserEntity user1 = new UserEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                Name = "大橙子",
                UserName = "cc",
                Nick = "橙子",
                Password = "123456",
                Email = "454313500@qq.com",
                Phone = 13800000000,
                Sex = SexEnum.Male,
                Address = "深圳",
                Age = 20,
                Introduction = "还有谁？",
                OrderNum = 999,
                Remark = "描述是什么呢？",
                State = true
            };
            user1.BuildPassword();
            entities.Add(user1);

            UserEntity user2 = new UserEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                Name = "大测试",
                UserName = "test",
                Nick = "测试",
                Password = "123456",
                Email = "454313500@qq.com",
                Phone = 15900000000,
                Sex = SexEnum.Woman,
                Address = "深圳",
                Age = 18,
                Introduction = "还有我！",
                OrderNum = 1,
                Remark = "我没有描述！",
                State = true
        
            };
            user2.BuildPassword();
            entities.Add(user2);

            return entities;
        }
    }
}
