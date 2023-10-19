using Furion.DependencyInjection;
using Yi.Framework.Infrastructure.Data.DataSeeds;
using Yi.Framework.Infrastructure.Ddd.Repositories;
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


            return entities;
        }
    }
}
