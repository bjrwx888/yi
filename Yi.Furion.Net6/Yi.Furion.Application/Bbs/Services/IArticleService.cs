using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.Bbs.Dtos.Article;

namespace Yi.Furion.Application.Bbs.Services
{
    /// <summary>
    /// Article服务抽象
    /// </summary>
    public interface IArticleService : ICrudAppService<ArticleGetOutputDto, ArticleGetListOutputDto, long, ArticleGetListInputVo, ArticleCreateInputVo, ArticleUpdateInputVo>
    {

    }
}
