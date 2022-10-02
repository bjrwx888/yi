using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface ILoginLogService:IBaseService<LoginLogEntity>
    {
        Task<PageModel<List<LoginLogEntity>>> SelctPageList(LoginLogEntity loginLog, PageParModel page);
    }
}
