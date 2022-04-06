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
using Yi.Framework.WebCore.AuthorizationPolicy;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseCrudController<UserEntity>
    {
        public UserController(ILogger<UserEntity> logger, IUserService iUserService) : base(logger, iUserService)
        {
          
        }
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok(await _iRepository.GetListAsync());
        }
    }
}
