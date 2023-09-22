using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion;
using Furion.DependencyInjection;
using Yi.Framework.Infrastructure.Data.DataSeeds;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Module.WebFirstManager.Entities;
using Yi.Framework.Module.WebFirstManager.Enums;

namespace Yi.Framework.Module.WebFirstManager.DataSeed
{
    public class FieldDataSeed : AbstractDataSeed<FieldEntity>, ITransient
    {
        private TableEntity _tableEntity;
        public FieldDataSeed(IRepository<FieldEntity> repository) : base(repository)
        {
        }

        public override async Task<bool> IsInvoker()
        {
            var tableRepository = App.GetRequiredService<IRepository<TableEntity>>();
             _tableEntity = await tableRepository.GetFirstAsync(x => x.Name == "Test");
            if (_tableEntity is null)
            {
                return false;
            }
          return  await base.IsInvoker();
        }

        public override List<FieldEntity> GetSeedData()
        {


            var entities = new List<FieldEntity>();
            entities.Add(new FieldEntity
            {
                FieldType = FieldTypeEnum.String,
                Description = "测试字段",
                Name = "DDD",
                Length = 100,
                TableId = _tableEntity.Id

            });
            return entities;
        }
    }
}
