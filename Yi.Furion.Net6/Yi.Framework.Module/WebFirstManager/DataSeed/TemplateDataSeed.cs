using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Yi.Framework.Infrastructure.Data.DataSeeds;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Helper;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.DataSeed
{
    public class TemplateDataSeed : AbstractDataSeed<TemplateEntity>, ITransient
    {
        public TemplateDataSeed(IRepository<TemplateEntity> repository) : base(repository)
        {
        }

        public override List<TemplateEntity> GetSeedData()
        {
            var entities = new List<TemplateEntity>();
            entities.Add(new TemplateEntity
            {
                Id = SnowflakeHelper.NextId,
                TemplateStr = "你好世界 :@model",
                BuildPath = ""
            });

            entities.Add(new TemplateEntity
            {
                Id = SnowflakeHelper.NextId,
                TemplateStr = "你好世界2 :@Model",
                BuildPath = ""
            });


            return entities;
        }
    }
}
