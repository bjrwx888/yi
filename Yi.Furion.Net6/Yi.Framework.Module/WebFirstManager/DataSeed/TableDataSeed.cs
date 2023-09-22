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
    public class TableDataSeed : AbstractDataSeed<TableEntity>, ITransient
    {
        public TableDataSeed(IRepository<TableEntity> repository) : base(repository)
        {
        }

        public override List<TableEntity> GetSeedData()
        {
            var entities=new List<TableEntity>();

            entities.Add(new TableEntity
            {
                Id=SnowflakeHelper.NextId,
                Name="Test",
                Description="测试",
            });
            return entities;
        }
    }
}
