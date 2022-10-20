using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface ISpuService:IBaseService<SpuEntity>
    {
        /// <summary>
        /// 动态条件分页查询
        /// </summary>
        /// <param name="eneity"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PageModel<List<SpuEntity>>> SelctPageList(SpuEntity entity, PageParModel page);
    }
}
