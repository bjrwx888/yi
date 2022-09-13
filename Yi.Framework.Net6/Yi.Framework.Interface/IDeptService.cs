using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IDeptService:IBaseService<DeptEntity>
    {
        /// <summary>
        /// 动态条件查询
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        Task<List<DeptEntity>> SelctGetList(DeptEntity dept);
    }
}
