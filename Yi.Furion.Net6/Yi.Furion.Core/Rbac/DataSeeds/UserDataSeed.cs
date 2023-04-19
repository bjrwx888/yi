using SqlSugar;
using Yi.Framework.Infrastructure.Data.DataSeeds;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Furion.Core.Rbac.Entities;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.DataSeeds
{
    ////支持依赖注入执行
    //[AppService(typeof(IDataSeed<UserEntity>))]

    ////支持启动时执行
    //[AppService(typeof(IDataSeed))]
    public class UserDataSeed : AbstractDataSeed<UserEntity>,ITransient
    {
        public UserDataSeed(IRepository<UserEntity> repository) : base(repository)
        {
        }

        public override List<UserEntity> GetSeedData()
        {
            var entities = new List<UserEntity>();
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

            UserEntity user3 = new UserEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                Name = "游客",
                UserName = "guest",
                Nick = "测试",
                Password = "123456",
                Email = "454313500@qq.com",
                Phone = 15900000000,
                Sex = SexEnum.Woman,
                Address = "深圳",
                Age = 18,
                Introduction = "临时游客",
                OrderNum = 1,
                Remark = "懒得创账号",
                State = true

            };
            user3.BuildPassword();
            entities.Add(user3);

            return entities;
        }
    }
}
