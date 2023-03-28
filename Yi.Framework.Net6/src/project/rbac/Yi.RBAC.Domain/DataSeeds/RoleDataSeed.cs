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
    [AppService(typeof(IDataSeed))]
    public class RoleDataSeed : AbstractDataSeed<RoleEntity>
    {
        public RoleDataSeed(IRepository<RoleEntity> repository) : base(repository)
        {
        }

        public override List<RoleEntity> GetSeedData()
        {
            var entities = new List<RoleEntity>();
            RoleEntity role1 = new RoleEntity()
            {
                Id = SnowflakeHelper.NextId,
                RoleName = "管理员",
                RoleCode = "admin",
                DataScope = DataScopeEnum.ALL,
                OrderNum = 999,
                Remark = "管理员",
                IsDeleted = false
            };
            entities.Add(role1);

            RoleEntity role2 = new RoleEntity()
            {
                Id = SnowflakeHelper.NextId,
                RoleName = "测试角色",
                RoleCode = "test",
                DataScope = DataScopeEnum.ALL,
                OrderNum = 1,
                Remark = "测试用的角色",
                IsDeleted = false
            };
            entities.Add(role2);

            RoleEntity role3 = new RoleEntity()
            {
                Id = SnowflakeHelper.NextId,
                RoleName = "普通用户",
                RoleCode = "common",
                DataScope = DataScopeEnum.ALL,
                OrderNum = 1,
                Remark = "正常用户",
                IsDeleted = false
            };
            entities.Add(role3);

            RoleEntity role4 = new RoleEntity()
            {
                Id = SnowflakeHelper.NextId,
                RoleName = "游客用户",
                RoleCode = "guest",
                DataScope = DataScopeEnum.ALL,
                OrderNum = 1,
                Remark = "可简单浏览",
                IsDeleted = false
            };
            entities.Add(role4);


            return entities;
        }
    }
}
