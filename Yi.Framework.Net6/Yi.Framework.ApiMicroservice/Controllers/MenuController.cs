﻿using Microsoft.AspNetCore.Authorization;
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
    public class MenuController 
    {
        private IMenuService _iMenuService;
        public MenuController(ILogger<MenuEntity> logger, IMenuService iMenuService)
        {
            _iMenuService = iMenuService;
        }

        [HttpGet]
        public async Task<Result> GetList()
        {
            return Result.Success().SetData(await _iMenuService._repository.GetListAsync());
        }


        /// <summary>
        /// 得到树形菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetMenuTree()
        { 
             return Result.Success().SetData(await _iMenuService. GetMenuTreeAsync());
        }
    }
}
