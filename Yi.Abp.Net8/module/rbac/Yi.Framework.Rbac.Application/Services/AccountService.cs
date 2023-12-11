using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Lazy.Captcha.Core;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Guids;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using Yi.Framework.Rbac.Application.Contracts.Dtos.Account;
using Yi.Framework.Rbac.Application.Contracts.IServices;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.Rbac.Domain.Managers;
using Yi.Framework.Rbac.Domain.Repositories;
using Yi.Framework.Rbac.Domain.Shared.Caches;
using Yi.Framework.Rbac.Domain.Shared.Consts;
using Yi.Framework.Rbac.Domain.Shared.Dtos;
using Yi.Framework.Rbac.Domain.Shared.Etos;
using Yi.Framework.Rbac.Domain.Shared.Options;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Rbac.Application.Services
{

    public class AccountService : ApplicationService, IAccountService
    {
        private readonly ILocalEventBus _localEventBus;
        private readonly JwtOptions _jwtOptions;
        private IDistributedCache<CaptchaPhoneCacheItem, CaptchaPhoneCacheKey> _phoneCache;
        private readonly ICaptcha _captcha;
        private readonly IGuidGenerator _guidGenerator;
        public AccountService(IUserRepository userRepository,
            ICurrentUser currentUser,
            AccountManager accountManager,
            ISqlSugarRepository<MenuEntity> menuRepository,
            IHttpContextAccessor httpContextAccessor,
            ILocalEventBus localEventBus,
            IOptions<JwtOptions> jwtOptions,
            IDistributedCache<CaptchaPhoneCacheItem, CaptchaPhoneCacheKey> phoneCache,
            ICaptcha captcha,
            IGuidGenerator guidGenerator)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _accountManager = accountManager;
            _menuRepository = menuRepository;
            _httpContextAccessor = httpContextAccessor;
            _localEventBus = localEventBus;
            _jwtOptions = jwtOptions.Value;
            _phoneCache = phoneCache;
            _captcha = captcha;
            _guidGenerator = guidGenerator;
        }


        private IUserRepository _userRepository;
        private ICurrentUser _currentUser;
        private AccountManager _accountManager;
        private ISqlSugarRepository<MenuEntity> _menuRepository;
        private IUserService _userService;
        private UserManager _userManager;
        private ISqlSugarRepository<RoleEntity> _roleRepository;
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 效验图片登录验证码,无需和账号绑定
        /// </summary>
        private void ValidationImageCaptcha(LoginInputVo input)
        {
            //登录不想要验证码 ，可不效验
            if (!_captcha.Validate(input.Uuid, input.Code))
            {
                throw new UserFriendlyException("验证码错误");
            }
        }

        /// <summary>
        /// 效验电话验证码，需要与电话号码绑定
        /// </summary>
        private void ValidationPhoneCaptcha(RegisterDto input)
        {
            //var value = _memoryCache.Get<string>($"Yi:Phone:{input.Phone}");
            //if (value is not null && value.Equals($"{input.Code}"))
            //{
            //    //成功，需要清空
            //    _memoryCache.Remove($"Yi:Phone:{input.Phone}");
            //    return;
            //}
            //throw new UserFriendlyException("验证码错误");
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
            //  ValidationImageCaptcha(input);

            UserEntity user = new();
            //登录成功
            await _accountManager.LoginValidationAsync(input.UserName, input.Password, x => user = x);

            //获取用户信息
            var userInfo = await _userRepository.GetUserAllInfoAsync(user.Id);

            if (userInfo.RoleCodes.Count == 0)
            {
                throw new UserFriendlyException(UserConst.No_Role);
            }
            //这里抛出一个登录的事件
            var loginEntity = new LoginLogEntity().GetInfoByHttpContext(_httpContextAccessor.HttpContext);
            var loginEto = loginEntity.Adapt<LoginEventArgs>();
            loginEto.UserName = input.UserName;
            loginEto.UserId = userInfo.User.Id;
            await _localEventBus.PublishAsync(loginEto);
            //将用户信息添加到缓存中，需要考虑的是更改了用户、角色、菜单等整个体系都需要将缓存进行刷新，看具体业务进行选择



            //创建token
            var accessToken = CreateToken(_accountManager.UserInfoToClaim(userInfo));
            return new { Token = accessToken };
        }

        /// <summary>
        /// 创建令牌
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private string CreateToken(Dictionary<string, object> dic)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = dic.Select(x => new Claim(x.Key, x.Value.ToString())).ToList();
            var token = new JwtSecurityToken(
               issuer: _jwtOptions.Issuer,
               audience: _jwtOptions.Audience,
               claims: claims,
               expires: DateTime.Now.AddSeconds(_jwtOptions.ExpiresMinuteTime),
               notBefore: DateTime.Now,
               signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);

            return returnToken;
        }


        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        public async Task<CaptchaImageDto> GetCaptchaImageAsync()
        {
            var uuid = _guidGenerator.Create();
            var captcha = _captcha.Generate(uuid.ToString());
            return new CaptchaImageDto { Img = captcha.Bytes, Uuid = uuid };
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
            var value = await _phoneCache.GetAsync(new CaptchaPhoneCacheKey(input.Phone));

            //防止暴刷
            if (value is not null)
            {
                throw new UserFriendlyException($"{input.Phone}已发送过验证码，10分钟后可重试");
            }
            //生成一个4位数的验证码
            //发送短信，同时生成uuid
            ////key： 电话号码  value:验证码+uuid  
            //var code = _securityCode.GetRandomEnDigitalText(4);
            var uuid = Guid.NewGuid();

            //未开启短信验证，默认8888
            //if (_smsAliyunManagerOptions.Value.EnableFeature)
            //{
            //    await _smsAliyunManager.Send(input.Phone, code);
            //}
            //else
            //{
            var code = "8888";
            //}
            //_memoryCache.Set($"Yi:Phone:{input.Phone}", $"{code}", new TimeSpan(0, 10, 0));

            await _phoneCache.SetAsync(new CaptchaPhoneCacheKey(input.Phone), new CaptchaPhoneCacheItem(code), new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(10) });
            return new
            {
                Uuid = uuid
            };
        }

        /// <summary>
        /// 注册，需要验证码通过
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [UnitOfWork]
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

            var newUser = new UserEntity(input.UserName, input.Password, input.Phone);

            var entity = await _userRepository.InsertReturnEntityAsync(newUser);
            //赋上一个初始角色
            var roleRepository = _roleRepository;
            var role = await roleRepository.GetFirstAsync(x => x.RoleCode == UserConst.GuestRoleCode);
            if (role is not null)
            {
                await _userManager.GiveUserSetRoleAsync(new List<Guid> { entity.Id }, new List<Guid> { role.Id });
            }
            return true;
        }


        /// <summary>
        /// 查询已登录的账户信息
        /// </summary>
        /// <returns></returns>
        [Route("account")]
        [Authorize]

        public async Task<UserRoleMenuDto> Get()
        {
            //通过鉴权jwt获取到用户的id
            var userId = _currentUser.Id;
            if (userId is null)
            {
                throw new UserFriendlyException("用户未登录");
            }
            //此处从缓存中获取即可
            //var data = _cacheManager.Get<UserRoleMenuDto>($"Yi:UserInfo:{userId}");
            await Console.Out.WriteLineAsync(userId.ToString() + "99999999");
            var data = await _userRepository.GetUserAllInfoAsync(userId ?? Guid.Empty);
            //系统用户数据被重置，老前端访问重新授权
            if (data is null)
            {
                throw new AbpAuthorizationException();
            }

            data.Menus.Clear();
            return data;
        }


        /// <summary>
        /// 获取当前登录用户的前端路由
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("account/Vue3Router")]
        public async Task<List<Vue3RouterDto>> GetVue3Router()
        {
            var userId = _currentUser.Id;
            if (_currentUser.Id is null)
            {
                throw new AbpAuthorizationException("用户未登录");

            }
            var data = await _userRepository.GetUserAllInfoAsync(userId ?? Guid.Empty);
            var menus = data.Menus.ToList();

            //为超级管理员直接给全部路由
            if (UserConst.Admin.Equals(data.User.UserName))
            {
                menus = ObjectMapper.Map<List<MenuEntity>, List<MenuDto>>(await _menuRepository.GetListAsync());
            }
            //将后端菜单转换成前端路由，组件级别需要过滤
            List<Vue3RouterDto> routers = ObjectMapper.Map<List<MenuDto>, List<MenuEntity>>(menus).Vue3RouterBuild();
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
            if (_currentUser.Id is null)
            {
                throw new UserFriendlyException("用户未登录");
            }
            await _accountManager.UpdatePasswordAsync(_currentUser.Id ?? Guid.Empty, input.NewPassword, input.OldPassword);
            return true;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> RestPasswordAsync(Guid userId, RestPasswordDto input)
        {
            if (string.IsNullOrEmpty(input.Password))
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
