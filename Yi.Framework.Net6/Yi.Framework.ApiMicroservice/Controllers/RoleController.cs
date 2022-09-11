﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DTOModel;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;
using Yi.Framework.Service;
using Yi.Framework.WebCore;
using Yi.Framework.WebCore.AttributeExtend;
using Yi.Framework.WebCore.AuthorizationPolicy;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : BaseSimpleRdController<RoleEntity>
    {
        private IRoleService _iRoleService;
        public RoleController(ILogger<RoleEntity> logger, IRoleService iRoleService) : base(logger, iRoleService)
        {
            _iRoleService = iRoleService;
        }

        /// <summary>
        /// 动态条件分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> PageList([FromQuery] RoleEntity role, [FromQuery] PageParModel page)
        {
            return Result.Success().SetData(await _iRoleService.SelctPageList(role, page));
        }


        /// <summary>
        /// 给多用户设置多角色
        /// </summary>
        /// <param name="giveRoleSetMenuDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> GiveRoleSetMenu(GiveRoleSetMenuDto giveRoleSetMenuDto)
        {
            return Result.Success().SetStatus(await _iRoleService.GiveRoleSetMenu(giveRoleSetMenuDto.RoleIds, giveRoleSetMenuDto.MenuIds));
        }


        /// <summary>
        /// 添加角色包含菜单
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<Result> Add(RoleInfoDto roleDto)
        {
            return Result.Success().SetData(await _iRoleService.AddInfo(roleDto));
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> Update(RoleInfoDto roleDto)
        {
            return Result.Success().SetStatus(await _iRoleService.UpdateInfo(roleDto));
        }
    }
}
