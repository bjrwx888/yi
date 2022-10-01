using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class OperationLogService : BaseService<OperationLogEntity>, IOperationLogService
    {
        public OperationLogService(IRepository<OperationLogEntity> repository) : base(repository)
        {
        }
    }
}
