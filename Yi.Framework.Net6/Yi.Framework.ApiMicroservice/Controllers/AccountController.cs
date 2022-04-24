using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Core;
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
    /// 账户控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController :ControllerBase
    {
        private  IUserService _iUserService;
        private JwtInvoker _jwtInvoker;
        public AccountController(ILogger<UserEntity> logger, IUserService iUserService, JwtInvoker jwtInvoker)
        {
            _iUserService = iUserService;
            _jwtInvoker = jwtInvoker;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> Login(LoginDto loginDto)
        {
            UserEntity user=new();
            if (await _iUserService.Login(loginDto.UserName, loginDto.Password,o=> user=o))
            {
                return Result.Success("登录成功！").SetData(new { user, token = _jwtInvoker.GetAccessToken(user)});
            }
            return Result.SuccessError("登录失败！用户名或者密码错误！");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> Register(RegisterDto registerDto)
        {
            UserEntity user = new();
            if (await _iUserService.Register(WebCore.Mapper.MapperHelper.Map<UserEntity, RegisterDto>(registerDto), o => user = o))
            {
                return Result.Success("注册成功！").SetData(user);
            }
            return Result.SuccessError("注册失败！用户名已存在！");
        }
    }
}
