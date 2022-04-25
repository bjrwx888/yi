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
    /// 用户管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseCrudController<UserEntity>
    {
        private IUserService _iUserService;
        public UserController(ILogger<UserEntity> logger, IUserService iUserService) : base(logger, iUserService)
        {
            _iUserService = iUserService;
        }

        /// <summary>
        /// 给多用户设置多角色
        /// </summary>
        /// <param name="giveUserSetRoleDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> GiveUserSetRole(GiveUserSetRoleDto giveUserSetRoleDto)
        {
           return Result.Success().SetStatus(await _iUserService.GiveUserSetRole(giveUserSetRoleDto.UserIds,giveUserSetRoleDto.RoleIds));
        }
    }
}
