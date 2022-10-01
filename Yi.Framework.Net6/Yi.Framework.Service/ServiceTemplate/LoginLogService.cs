using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class LoginLogService : BaseService<LoginLogEntity>, ILoginLogService
    {
        public LoginLogService(IRepository<LoginLogEntity> repository) : base(repository)
        {
        }
    }
}
