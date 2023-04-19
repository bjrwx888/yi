using Yi.Framework.Infrastructure.Data.DataSeeds;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Core.Rbac.DataSeeds
{
    public class PostDataSeed : AbstractDataSeed<PostEntity>, ITransient
    {
        public PostDataSeed(IRepository<PostEntity> repository) : base(repository)
        {
        }

        public override List<PostEntity> GetSeedData()
        {
            var entites = new List<PostEntity>();

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
