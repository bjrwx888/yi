using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class DeptService : BaseService<DeptEntity>, IDeptService
    {
        public DeptService(IRepository<DeptEntity> repository) : base(repository)
        {
        }
    }
}
