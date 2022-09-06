using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class DictionaryService : BaseService<DictionaryEntity>, IDictionaryService
    {
        public DictionaryService(IRepository<DictionaryEntity> repository) : base(repository)
        {
        }
    }
}
