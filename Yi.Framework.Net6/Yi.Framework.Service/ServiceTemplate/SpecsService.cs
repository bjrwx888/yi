using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class SpecsService : BaseService<SpecsEntity>, ISpecsService
    {
        public SpecsService(IRepository<SpecsEntity> repository) : base(repository)
        {
        }
    }
}
