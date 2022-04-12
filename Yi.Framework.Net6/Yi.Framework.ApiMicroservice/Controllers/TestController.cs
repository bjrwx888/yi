using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Language;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;
using Yi.Framework.WebCore;
using Yi.Framework.WebCore.AttributeExtend;
using Yi.Framework.WebCore.AuthorizationPolicy;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private IStringLocalizer<LocalLanguage> _local;
        private IUserService _iUserService;
        public TestController(ILogger<UserEntity> logger, IUserService iUserService, IStringLocalizer<LocalLanguage> local) 
        {
            _local = local;
            _iUserService = iUserService;
        }

        /// <summary>
        /// 仓储上下文对象测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        /// 特点：仓储代理上下文对象，用起来就是爽
        public async Task<Result> DbTest()
        {
            return Result.Success().SetData(await _iUserService.DbTest());
        }

        /// <summary>
        /// 国际化测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result LocalTest()
        {
            return Result.Success().SetData(_local["succeed"]);
        }

        /// <summary>
        /// 权限测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Permission("user:get:test")]
        public Result PermissionTest()
        {
            return Result.Success();
        }

        /// <summary>
        /// 策略授权测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(PolicyName.Sid)]
        public Result AutnTest()
        {
            return Result.Success();
        }
    }
}
