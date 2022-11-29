using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DTOModel.Vo;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;
using Yi.Framework.Service;
using Yi.Framework.WebCore;
using Yi.Framework.WebCore.AttributeExtend;
using Yi.Framework.WebCore.AuthorizationPolicy;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AgreeController : ControllerBase
    {
        [Autowired]
        public IAgreeService _iAgreeService { get; set; }
        [Autowired]
        public IArticleService _iArticleService { get; set; }
        [Autowired]
        public ILogger<AgreeEntity> _logger { get; set; }

        /// <summary>
        /// 点赞操作
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> Operate(long articleId)
        {
            //long userId = HttpContext.GetUserIdInfo();
            long userId = 1L;
            var article = await _iArticleService._repository.GetByIdAsync(articleId);
            if (await _iAgreeService._repository.IsAnyAsync(u => u.UserId == userId && u.ArticleId == articleId))
            {
                //已点赞，取消点赞
                await _iAgreeService._repository.UseTranAsync(async () =>
                {
                    await _iAgreeService._repository.DeleteAsync(u => u.UserId == userId && u.ArticleId == articleId);
                    await _iArticleService._repository.UpdateIgnoreNullAsync(new ArticleEntity { Id = articleId, AgreeNum = article.AgreeNum - 1 });

                });
            }
            else
            {
                //未点赞，添加点赞记录
                await _iAgreeService._repository.UseTranAsync(async () =>
                {
                    await _iAgreeService._repository.InsertAsync(new AgreeEntity { UserId = userId, ArticleId = articleId });
                    await _iArticleService._repository.UpdateIgnoreNullAsync(new ArticleEntity { Id = articleId, AgreeNum = article.AgreeNum + 1 });
                });

            }
            return Result.Success("这里业务全部拆开放service层去");
        }
    }
}
