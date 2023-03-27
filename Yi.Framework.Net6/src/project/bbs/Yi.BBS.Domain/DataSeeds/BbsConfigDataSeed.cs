using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.DataSeeds;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Setting.Entities;
using Yi.RBAC.Domain.Shared.Identity.EnumClasses;

namespace Yi.BBS.Domain.DataSeed
{
    [AppService(typeof(IDataSeed))]
    public class BbsConfigDataSeed : AbstractDataSeed<ConfigEntity>
    {
        public BbsConfigDataSeed(IRepository<ConfigEntity> repository) : base(repository)
        {
        }

        public override async Task<bool> IsInvoker()
        {
            return !await _repository.IsAnyAsync(x => x.ConfigKey == "bbs.site.name");
        }
        public override List<ConfigEntity> GetSeedData()
        {
            List<ConfigEntity> entities = new List<ConfigEntity>()
            {
                  new ConfigEntity { Id = SnowflakeHelper.NextId, ConfigKey = "bbs.site.name", ConfigValue = "Yi意社区", ConfigName = "bbs站点名称" },
                   new ConfigEntity { Id = SnowflakeHelper.NextId, ConfigKey = "bbs.site.author", ConfigValue = "橙子", ConfigName = "bbs站点作者" },
                    new ConfigEntity { Id = SnowflakeHelper.NextId, ConfigKey = "bbs.site.icp", ConfigValue = "2023 意社区 | 赣ICP备xxxxxx号-4", ConfigName = "bbs备案号" },
                     new ConfigEntity { Id = SnowflakeHelper.NextId, ConfigKey = "bbs.site.bottom", ConfigValue = "YiFramework意框架", ConfigName = "bbs底部信息" },
            };
            return entities;
        }
    }
}
