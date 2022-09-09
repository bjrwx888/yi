using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class TenantService : BaseService<TenantEntity>, ITenantService
    {
        public TenantService(IRepository<TenantEntity> repository) : base(repository)
        {
        }
    }
}
