using Hei.Captcha;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Enum;
using Yi.Framework.Common.Helper;
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
    /// 账户管理
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private IUserService _iUserService;
        private JwtInvoker _jwtInvoker;
        private ILogger _logger;
        private SecurityCodeHelper _securityCode;
        public AccountController(ILogger<UserEntity> logger, IUserService iUserService, JwtInvoker jwtInvoker, SecurityCodeHelper securityCode)
        {
            _iUserService = iUserService;
            _jwtInvoker = jwtInvoker;
            _logger = logger;
            _securityCode = securityCode;
        }

        /// <summary>
        ///  重置管理员CC的密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<Result> RestCC()
        {
            var user = await _iUserService._repository.GetFirstAsync(u => u.UserName == "cc");
            user.Password = "123456";
            user.BuildPassword();
            await _iUserService._repository.UpdateIgnoreNullAsync(user);
            return Result.Success();
        }

        /// <summary>
        /// 没啥说，登录
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> Login(LoginDto loginDto)
        {

            //跳过
            //先效验验证码和UUID

            UserEntity user = new();
            if (await _iUserService.Login(loginDto.UserName, loginDto.Password, o => user = o))
            {
                var userRoleMenu = await _iUserService.GetUserAllInfo(user.Id);
                return Result.Success("登录成功！").SetData(new { token = _jwtInvoker.GetAccessToken(userRoleMenu.User, userRoleMenu.Menus) });
            }
            return Result.Error("登录失败！用户名或者密码错误！");
        }



        /// <summary>
        /// 没啥说，注册
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 没啥说，登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Result Logout()
        {
            return Result.Success("安全登出成功！");
        }

        /// <summary>
        /// 通过已登录的用户获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        public async Task<Result> GetUserAllInfo()
        {
            //通过鉴权jwt获取到用户的id
            var userId = HttpContext.GetUserIdInfo();
            var data = await _iUserService.GetUserAllInfo(userId);
            data.Menus.Clear();
            return Result.Success().SetData(data);
        }

        /// <summary>
        /// 获取当前登录用户的前端路由
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetRouterInfo()
        {
            var userId = HttpContext.GetUserIdInfo();
            var data = await _iUserService.GetUserAllInfo(userId);

            //将后端菜单转换成前端路由，组件级别需要过滤
            List<VueRouterModel> routers = MenuEntity.RouterBuild(data.Menus.ToList());
            return Result.Success().SetData(routers);
        }

        /// <summary>
        /// 更新已登录用户的用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> UpdateUserByHttp(UserEntity user)
        {
            //当然，密码是不能给他修改的
            user.Password = null;
            user.Salt = null;

            //修改需要赋值上主键哦
            user.Id = HttpContext.GetUserIdInfo();
            return Result.Success().SetStatus(await _iUserService._repository.UpdateIgnoreNullAsync(user));
        }

        /// <summary>
        /// 自己更新密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> UpdatePassword(UpdatePasswordDto dto)
        {
            long userId = HttpContext.GetUserIdInfo();

            if (await _iUserService.UpdatePassword(dto, userId))
            {
                return Result.Success();
            }
            return Result.Error("更新失败！");
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public Result CaptchaImage()
        {
            var uuid = Guid.NewGuid();
            var code = _securityCode.GetRandomEnDigitalText(4);
            //将uuid与code中心化保存起来，登录根据uuid比对即可
            var imgbyte = _securityCode.GetEnDigitalCodeByte(code);
            return Result.Success().SetData(new { uuid = uuid, img = imgbyte });
        }
    }
}
