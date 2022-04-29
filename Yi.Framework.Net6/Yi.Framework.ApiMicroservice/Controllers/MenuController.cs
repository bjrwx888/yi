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
    /// <summary>
    /// 菜单管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MenuController : BaseCrudController<MenuEntity>
    {
        private IMenuService _iMenuService;
        public MenuController(ILogger<MenuEntity> logger, IMenuService iMenuService) : base(logger, iMenuService)
        {
            _iMenuService = iMenuService;
        }



        /// <summary>
        /// 得到树形菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //暂未制作逻辑删除与多租户的过滤
        public async Task<Result> GetMenuTree()
        { 
             return Result.Success().SetData(await _iMenuService. GetMenuTreeAsync());
        }
    }
}
