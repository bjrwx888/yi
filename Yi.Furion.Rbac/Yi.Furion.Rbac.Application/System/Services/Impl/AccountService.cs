using System.Text.RegularExpressions;
using Furion.EventBus;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SqlSugar;
using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Infrastructure.Exceptions;
using Yi.Framework.Infrastructure.Uow;
using Yi.Framework.Module.ImageSharp.HeiCaptcha;
using Yi.Framework.Module.Sms.Aliyun;
using Yi.Furion.Rbac.Application.System.Domain;
using Yi.Furion.Rbac.Application.System.Dtos.Account;
using Yi.Furion.Rbac.Core.ConstClasses;
using Yi.Furion.Rbac.Core.Dtos;
using Yi.Furion.Rbac.Core.Entities;
using Yi.Furion.Rbac.Core.Etos;
using Yi.Furion.Rbac.Sqlsugar.Core.Repositories;

namespace Yi.Furion.Rbac.Application.System.Services.Impl
{
    public class AccountService : ApplicationService, ITransient, IDynamicApiController
    {

        public AccountService(IUserRepository userRepository, ICurrentUser currentUser, AccountManager accountManager, IRepository<MenuEntity> menuRepository, SmsAliyunManager smsAliyunManager, IOptions<SmsAliyunOptions> smsAliyunManagerOptions, SecurityCodeHelper securityCode, IMemoryCache memoryCache, IEventPublisher eventPublisher) =>
            (_userRepository, _currentUser, _accountManager, _menuRepository, _smsAliyunManager, _smsAliyunManagerOptions, _securityCode, _memoryCache, _eventPublisher) =
            (userRepository, currentUser, accountManager, menuRepository, smsAliyunManager, smsAliyunManagerOptions, securityCode, memoryCache, eventPublisher);


        private IUserRepository _userRepository { get; set; }

        private ICurrentUser _currentUser { get; set; }

        private AccountManager _accountManager { get; set; }


        private IRepository<MenuEntity> _menuRepository { get; set; }


        private SecurityCodeHelper _securityCode { get; set; }


        private IEventPublisher _eventPublisher { get; set; }




        private IUserService _userService { get; set; }



        private UserManager _userManager { get; set; }


        private IUnitOfWorkManager _unitOfWorkManager { get; set; }


        private IRepository<RoleEntity> _roleRepository { get; set; }


        private IMemoryCache _memoryCache { get; set; }


        private SmsAliyunManager _smsAliyunManager { get; set; }


        private IOptions<SmsAliyunOptions> _smsAliyunManagerOptions { get; set; }

        /// <summary>
        /// 效验图片登录验证码,无需和账号绑定
        /// </summary>
        private void ValidationImageCaptcha(LoginInputVo input)
        {
            //登录不想要验证码 ，不效验
            return;
            var value = _memoryCache.Get<string>($"Yi:Captcha:{input.Code}");
            if (value is not null && value.Equals(input.Uuid))
            {
                return;
            }
            throw new UserFriendlyException("验证码错误");
        }

        /// <summary>
        /// 效验电话验证码，需要与电话号码绑定
        /// </summary>
        private void ValidationPhoneCaptcha(RegisterDto input)
        {
            var value = _memoryCache.Get<string>($"Yi:Phone:{input.Phone}");
            if (value is not null && value.Equals($"{input.Code}"))
            {
                //成功，需要清空
                _memoryCache.Remove($"Yi:Phone:{input.Phone}");
                return;
            }
            throw new UserFriendlyException("验证码错误");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<object> PostLoginAsync(LoginInputVo input)
        {
            if (string.IsNullOrEmpty(input.Password) || string.IsNullOrEmpty(input.UserName))
            {
                throw new UserFriendlyException("请输入合理数据！");
            }

            //效验验证码
            ValidationImageCaptcha(input);

            UserEntity user = new();
            //登录成功
            await _accountManager.LoginValidationAsync(input.UserName, input.Password, x => user = x);

            //获取用户信息
            var userInfo = await _userRepository.GetUserAllInfoAsync(user.Id);

            if (userInfo.RoleCodes.Count == 0)
            {
                throw new UserFriendlyException(UserConst.用户无角色分配);
            }
            //这里抛出一个登录的事件

            //不阻碍执行，无需等待
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法

            _eventPublisher.PublishAsync(new LoginEventSource(new LoginEventArgs
            {

                UserId = userInfo.User.Id,
                UserName = user.UserName

            })
       );
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法

            //创建token
            var accessToken = JWTEncryption.Encrypt(_accountManager.UserInfoToClaim(userInfo));
            return new { Token = accessToken };
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
            _memoryCache.Set($"Yi:Captcha:{code}", uuid, new TimeSpan(0, 10, 0));
            var imgbyte = _securityCode.GetEnDigitalCodeByte(code);
            return new CaptchaImageDto { Img = imgbyte, Code = code, Uuid = uuid };
        }

        /// <summary>
        /// 验证电话号码
        /// </summary>
        /// <param name="str_handset"></param>
        private async Task ValidationPhone(string str_handset)
        {
            var res = Regex.IsMatch(str_handset, "^(0\\d{2,3}-?\\d{7,8}(-\\d{3,5}){0,1})|(((13[0-9])|(15([0-3]|[5-9]))|(18[0-9])|(17[0-9])|(14[0-9]))\\d{8})$");
            if (res == false)
            {
                throw new UserFriendlyException("手机号码格式错误！请检查");
            }
            if (await _userRepository.IsAnyAsync(x => x.Phone.ToString() == str_handset))
            {
                throw new UserFriendlyException("该手机号已被注册！");

            }

        }


        /// <summary>
        /// 注册 手机验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<object> PostCaptchaPhone(PhoneCaptchaImageDto input)
        {
            await ValidationPhone(input.Phone);
            var value = _memoryCache.Get<string>($"Yi:Phone:{input.Phone}");

            //防止暴刷
            if (value is not null)
            {
                throw new UserFriendlyException($"{input.Phone}已发送过验证码，10分钟后可重试");
            }
            //生成一个4位数的验证码
            //发送短信，同时生成uuid
            //key： 电话号码  value:验证码+uuid  
            var code = _securityCode.GetRandomEnDigitalText(4);
            var uuid = Guid.NewGuid();

            //未开启短信验证，默认8888
            if (_smsAliyunManagerOptions.Value.EnableFeature)
            {
                await _smsAliyunManager.Send(input.Phone, code);
            }
            else
            {
                code = "8888";
            }
            _memoryCache.Set($"Yi:Phone:{input.Phone}", $"{code}", new TimeSpan(0, 10, 0));


            return new { Uuid = uuid };
        }

        /// <summary>
        /// 注册，需要验证码通过
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<object> PostRegisterAsync(RegisterDto input)
        {
            if (input.UserName == UserConst.Admin)
            {
                throw new UserFriendlyException("用户名无效注册！");
            }

            if (input.UserName.Length < 2)
            {
                throw new UserFriendlyException("账号名需大于等于2位！");
            }
            if (input.Password.Length < 6)
            {
                throw new UserFriendlyException("密码需大于等于6位！");
            }
            //效验验证码，根据电话号码获取 value，比对验证码已经uuid
            ValidationPhoneCaptcha(input);



            //输入的用户名与电话号码都不能在数据库中存在
            UserEntity user = new();
            var isExist = await _userRepository.IsAnyAsync(x =>
                  x.UserName == input.UserName
               || x.Phone == input.Phone);
            if (isExist)
            {
                throw new UserFriendlyException("用户已存在，注册失败");
            }
            using (var uow = _unitOfWorkManager.CreateContext())
            {
                var newUser = new UserEntity(input.UserName, input.Password, input.Phone);

                var entity = await _userRepository.InsertReturnEntityAsync(newUser);
                //赋上一个初始角色
                var roleRepository = _roleRepository;
                var role = await roleRepository.GetFirstAsync(x => x.RoleCode == UserConst.GuestRoleCode);
                if (role is not null)
                {
                    await _userManager.GiveUserSetRoleAsync(new List<long> { entity.Id }, new List<long> { role.Id });
                }
                uow.Commit();
            }

            return true;
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
            //var data = _cacheManager.Get<UserRoleMenuDto>($"Yi:UserInfo:{userId}");
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
        ///  更新头像
        /// </summary>
        /// <param name="input"></param>
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
