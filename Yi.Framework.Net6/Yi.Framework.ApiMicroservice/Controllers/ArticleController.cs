using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;
using Yi.Framework.WebCore;
using Yi.Framework.WebCore.AttributeExtend;
using Yi.Framework.WebCore.AuthorizationPolicy;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ArticleController : BaseSimpleCrudController<ArticleEntity>
    {
        private IArticleService _iArticleService;
        public ArticleController(ILogger<ArticleEntity> logger, IArticleService iArticleService) : base(logger, iArticleService)
        {
            _iArticleService = iArticleService;
        }

        /// <summary>
        /// 动态条件分页查询
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> PageList([FromQuery] ArticleEntity entity, [FromQuery] PageParModel page)
        {
            return Result.Success().SetData(await _iArticleService.SelctPageList(entity, page));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Task<Result> Add(ArticleEntity entity)
        {
            entity.UserId=HttpContext.GetUserIdInfo();
            return base.Add(entity);
        }
    }
}
