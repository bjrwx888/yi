using Cike.EventBus.DistributedEvent;
using Hei.Captcha;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cike.AutoWebApi.Setting;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Auth.JwtBearer.Authentication;
using Yi.Framework.Auth.JwtBearer.Authorization;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Core.Enums;
using Yi.Framework.Core.Exceptions;
using Yi.Framework.Ddd.Repositories;
using Yi.Framework.Ddd.Services;
using Yi.Framework.ThumbnailSharp;
using Yi.RBAC.Application.Contracts.Identity;
using Yi.RBAC.Application.Contracts.Identity.Dtos.Account;
using Yi.RBAC.Domain.Identity;
using Yi.RBAC.Domain.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Identity.Repositories;
using Yi.RBAC.Domain.Shared.Identity.ConstClasses;
using Yi.RBAC.Domain.Shared.Identity.Dtos;
using Yi.RBAC.Domain.Shared.Identity.Etos;
using System.Net.WebSockets;

namespace Yi.RBAC.Application.Identity
{
    [AppService]
    public class AccountService : ApplicationService, IAutoApiService
    {
        [Autowired]
        private JwtTokenManager _jwtTokenManager { get; set; }
        [Autowired]
        private IUserRepository _userRepository { get; set; }
        [Autowired]
        private ICurrentUser _currentUser { get; set; }
        [Autowired]
        private AccountManager _accountManager { get; set; }

        [Autowired]
        private IRepository<MenuEntity> _menuRepository { get; set; }

        [Autowired]
        private SecurityCodeHelper _securityCode { get; set; }

        [Autowired]
        private IDistributedEventBus _distributedEventBus { get; set; }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<object> PostLoginAsync(LoginInputVo input)
        {
            UserEntity user = new();
            //登录成功
            await _accountManager.LoginValidationAsync(input.UserName, input.Password, x => user = x);

            //获取用户信息
            var userInfo = await _userRepository.GetUserAllInfoAsync(user.Id);

            if (userInfo.PermissionCodes.Count == 0)
            {
                throw new UserFriendlyException(UserConst.用户无权限分配);
            }
            //这里抛出一个登录的事件

            //不阻碍执行，无需等待
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            _distributedEventBus.PublishAsync(new LoginEventArgs
            {
                UserId = userInfo.User.Id,
                UserName = user.UserName
            });
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法

            //创建token
            var token = _jwtTokenManager.CreateToken(_accountManager.UserInfoToClaim(userInfo));
            return new { Token = token };
        }

        /// <summary>
        /// 查询已登录的账户信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AuthException"></exception>
        [Route("/api/account")]
        [Authorize]
        public async Task<UserRoleMenuDto> Get()
        {
            //通过鉴权jwt获取到用户的id
            var userId = _currentUser.Id;
            //此处从缓存中获取即可
            //var data = _cacheDb.Get<UserRoleMenuDto>($"Yi:UserInfo:{userId}");
            var data = await _userRepository.GetUserAllInfoAsync(userId);
            //系统用户数据被重置，老前端访问重新授权
            if (data is null)
            {
                throw new AuthException();
            }

            data.Menus.Clear();
            return data;
        }


        /// <summary>
        /// 获取当前登录用户的前端路由
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<List<Vue3RouterDto>> GetVue3Router()
        {
            var userId = _currentUser.Id;
            var data = await _userRepository.GetUserAllInfoAsync(userId);
            var menus = data.Menus.ToList();

            //为超级管理员直接给全部路由
            if (UserConst.Admin.Equals(data.User.UserName))
            {
                menus = await _menuRepository.GetListAsync();
            }
            //将后端菜单转换成前端路由，组件级别需要过滤
            List<Vue3RouterDto> routers = menus.Vue3RouterBuild();
            return routers;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public Task<bool> PostLogout()
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        public CaptchaImageDto GetCaptchaImage()
        {
            var uuid = Guid.NewGuid();
            var code = _securityCode.GetRandomEnDigitalText(4);
            //将uuid与code，Redis缓存中心化保存起来，登录根据uuid比对即可
            //10分钟过期
            //_cacheDb.Set($"Yi:Captcha:{uuid}", code, new TimeSpan(0, 10, 0));
            var imgbyte = _securityCode.GetEnDigitalCodeByte(code);
            return new CaptchaImageDto { Img = imgbyte, Uuid = code };
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePasswordAsync(UpdatePasswordDto input)
        {
            if (input.OldPassword.Equals(input.NewPassword))
            {
                throw new UserFriendlyException("无效更新！输入的数据，新密码不能与老密码相同");
            }
            await _accountManager.UpdatePasswordAsync(_currentUser.Id, input.NewPassword, input.OldPassword);
            return true;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> RestPasswordAsync(long userId, RestPasswordDto input)
        {
            if (!string.IsNullOrEmpty(input.Password))
            {
                throw new UserFriendlyException("重置密码不能为空！");
            }
            await _accountManager.RestPasswordAsync(userId, input.Password);
            return true;
        }

        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public async Task<bool> UpdateIconAsync(UpdateIconDto input)
        {
            var entity = await _userRepository.GetByIdAsync(_currentUser.Id);
            entity.Icon = input.Icon;
            await _userRepository.UpdateAsync(entity);

            return true;
        }
    }
}
