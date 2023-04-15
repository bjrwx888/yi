using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.DataSeeds;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Entities;

namespace Yi.RBAC.Domain.DataSeeds
{
    [AppService(typeof(IDataSeed))]
    public class DeptDataSeed : AbstractDataSeed<DeptEntity>
    {
        public DeptDataSeed(IRepository<DeptEntity> repository) : base(repository)
        {
        }

        public override List<DeptEntity> GetSeedData()
        {
            var entities =new List<DeptEntity>();

            DeptEntity chengziDept = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "橙子科技",
                DeptCode = "Yi",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = 0,
                Leader = "橙子",
                Remark = "如名所指"
            };
            entities.Add(chengziDept);


            DeptEntity shenzhenDept = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "深圳总公司",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = chengziDept.Id
            };
            entities.Add(shenzhenDept);


            DeptEntity jiangxiDept = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "江西总公司",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = chengziDept.Id
            };
            entities.Add(jiangxiDept);



            DeptEntity szDept1 = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "研发部门",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = shenzhenDept.Id
            };
            entities.Add(szDept1);

            DeptEntity szDept2 = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "市场部门",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = shenzhenDept.Id
            };
            entities.Add(szDept2);

            DeptEntity szDept3 = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "测试部门",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = shenzhenDept.Id
            };
            entities.Add(szDept3);

            DeptEntity szDept4 = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "财务部门",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = shenzhenDept.Id
            };
            entities.Add(szDept4);

            DeptEntity szDept5 = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "运维部门",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = shenzhenDept.Id
            };
            entities.Add(szDept5);


            DeptEntity jxDept1 = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "市场部门",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = jiangxiDept.Id
            };
            entities.Add(jxDept1);


            DeptEntity jxDept2 = new DeptEntity()
            {
                Id = SnowflakeHelper.NextId,
                DeptName = "财务部门",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = jiangxiDept.Id
            };
            entities.Add(jxDept2);


            return entities;
        }
    }
}
