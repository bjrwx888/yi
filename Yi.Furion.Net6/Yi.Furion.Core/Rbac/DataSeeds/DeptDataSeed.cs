using Yi.Framework.Infrastructure.Data.DataSeeds;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Core.Rbac.DataSeeds
{

    public class DeptDataSeed : AbstractDataSeed<DeptEntity>,ITransient
    {
        public DeptDataSeed(IRepository<DeptEntity> repository) : base(repository)
        {
        }

        public override List<DeptEntity> GetSeedData()
        {
            var entities = new List<DeptEntity>();

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
