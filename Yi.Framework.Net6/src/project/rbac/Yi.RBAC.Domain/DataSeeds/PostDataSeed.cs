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
    public class PostDataSeed : AbstractDataSeed<PostEntity>
    {
        public PostDataSeed(IRepository<PostEntity> repository) : base(repository)
        {
        }

        public override List<PostEntity> GetSeedData()
        {
            var entites=new List<PostEntity>();

            PostEntity Post1 = new PostEntity()
            {
                Id = SnowflakeHelper.NextId,
                PostName = "董事长",
                PostCode = "ceo",
                OrderNum = 100,
                IsDeleted = false
            };
            entites.Add(Post1);

            PostEntity Post2 = new PostEntity()
            {
                Id = SnowflakeHelper.NextId,
                PostName = "项目经理",
                PostCode = "se",
                OrderNum = 100,
                IsDeleted = false
            };
            entites.Add(Post2);

            PostEntity Post3 = new PostEntity()
            {
                Id = SnowflakeHelper.NextId,
                PostName = "人力资源",
                PostCode = "hr",
                OrderNum = 100,
                IsDeleted = false
            };
            entites.Add(Post3);

            PostEntity Post4 = new PostEntity()
            {
                Id = SnowflakeHelper.NextId,
                PostName = "普通员工",
                PostCode = "user",
                OrderNum = 100,
                IsDeleted = false
            };

            entites.Add(Post4);
            return entites;
        }
    }


}
