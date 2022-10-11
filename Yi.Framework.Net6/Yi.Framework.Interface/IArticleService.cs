using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IArticleService:IBaseService<ArticleEntity>
    {
        Task<PageModel<List<ArticleEntity>>> SelctPageList(ArticleEntity eneity, PageParModel page);
    }
}
