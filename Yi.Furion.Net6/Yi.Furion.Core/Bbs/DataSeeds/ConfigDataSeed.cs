using SqlSugar;
using Yi.Framework.Infrastructure.Data.DataSeeds;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Module.DictionaryManager.Entities;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Core.Bbs.DataSeeds
{
    public class ConfigDataSeed : AbstractDataSeed<ConfigEntity>, ITransient
    {
        public ConfigDataSeed(IRepository<ConfigEntity> repository) : base(repository)
        {
        }

        public override List<ConfigEntity> GetSeedData()
        {
            List<ConfigEntity> entities = new List<ConfigEntity>();
            ConfigEntity config1 = new ConfigEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
              ConfigKey= "bbs.site.name",
              ConfigName="站点名称",
              ConfigValue="意社区"
            };
            entities.Add(config1);

            ConfigEntity config2 = new ConfigEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                ConfigKey = "bbs.site.author",
                ConfigName = "站点作者",
                ConfigValue = "橙子"
            };
            entities.Add(config2);

            ConfigEntity config3 = new ConfigEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                ConfigKey = "bbs.site.icp",
                ConfigName = "站点Icp备案",
                ConfigValue = "赣ICP备20008025号"
            };
            entities.Add(config3);


            ConfigEntity config4 = new ConfigEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                ConfigKey = "bbs.site.bottom",
                ConfigName = "站点底部信息",
                ConfigValue = "你好世界"
            };
            entities.Add(config4);
            return entities;
        }
    }
}
