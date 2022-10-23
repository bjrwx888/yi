using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class SpecsGroupService : BaseService<SpecsGroupEntity>, ISpecsGroupService
    {
        public SpecsGroupService(IRepository<SpecsGroupEntity> repository) : base(repository)
        {
        }
    }
}
