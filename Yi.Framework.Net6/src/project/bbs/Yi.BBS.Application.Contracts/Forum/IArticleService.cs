using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.BBS.Application.Contracts.Forum
{
    /// <summary>
    /// Article服务抽象
    /// </summary>
    public interface IArticleService : ICrudAppService<ArticleGetOutputDto, ArticleGetListOutputDto, long, ArticleGetListInputVo, ArticleCreateInputVo, ArticleUpdateInputVo>
    {

    }
}
