using Microsoft.AspNetCore.Authorization;
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
    public class RoleController : BaseCrudController<RoleEntity>
    {
        private IRoleService _iRoleService;
        public RoleController(ILogger<RoleEntity> logger, IRoleService iRoleService) : base(logger, iRoleService)
        {
            _iRoleService = iRoleService;
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


    }
}
