using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class ConfigService : BaseService<ConfigEntity>, IConfigService
    {
        public ConfigService(IRepository<ConfigEntity> repository) : base(repository)
        {
        }
    }
}
