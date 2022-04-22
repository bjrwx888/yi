using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class LogService : BaseService<LogEntity>, ILogService
    {
        public LogService(IRepository<LogEntity> repository) : base(repository)
        {
        }
    }
}
