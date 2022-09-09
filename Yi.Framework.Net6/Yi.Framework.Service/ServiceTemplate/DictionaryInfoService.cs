using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class DictionaryInfoService : BaseService<DictionaryInfoEntity>, IDictionaryInfoService
    {
        public DictionaryInfoService(IRepository<DictionaryInfoEntity> repository) : base(repository)
        {
        }
    }
}
