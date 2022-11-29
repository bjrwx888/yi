using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class AgreeService : BaseService<AgreeEntity>, IAgreeService
    {
        public AgreeService(IRepository<AgreeEntity> repository) : base(repository)
        {
        }
    }
}
