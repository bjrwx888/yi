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
    public class SkuController : BaseSimpleCrudController<SkuEntity>
    {
        private ISkuService _iSkuService;
        public SkuController(ILogger<SkuEntity> logger, ISkuService iSkuService) : base(logger, iSkuService)
        {
            _iSkuService = iSkuService;
        }

        /// <summary>
        /// 动态条件分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> PageList([FromQuery] SkuEntity eneity, [FromQuery] PageParModel page)
        {
            return Result.Success().SetData(await _iSkuService.SelctPageList(eneity, page));
        }
    }
}
