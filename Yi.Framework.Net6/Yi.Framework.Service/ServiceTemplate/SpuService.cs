using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class SpuService : BaseService<SpuEntity>, ISpuService
    {
        public SpuService(IRepository<SpuEntity> repository) : base(repository)
        {
        }
    }
}
