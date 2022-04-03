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
using Yi.Framework.WebCore;
using Yi.Framework.WebCore.AuthorizationPolicy;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private IUserService _iUserService;
        public UserController(ILogger<UserController> logger, IUserService iUserService)
        {
            _logger = logger;
            _iUserService = iUserService;
        }

        [HttpGet]
        public async Task<Result> Get()
        {
            return Result.Success().SetData(await _iUserService.GetListAsync());
        }
        [HttpPost]
        public async Task<Result> Add(UserEntity userEntity)
        {
            return Result.Success().SetData(await _iUserService.InsertAsync(userEntity));
        }
    }
}
