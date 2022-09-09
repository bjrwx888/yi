using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IDictionaryService:IBaseService<DictionaryEntity>
    {
        /// <summary>
        /// 动态条件分页查询
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PageModel<List<DictionaryEntity>>> SelctPageList(DictionaryEntity dic, PageParModel page);
    }
}
