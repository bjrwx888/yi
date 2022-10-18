using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class SkuService : BaseService<SkuEntity>, ISkuService
    {
        public SkuService(IRepository<SkuEntity> repository) : base(repository)
        {
        }
    }
}
