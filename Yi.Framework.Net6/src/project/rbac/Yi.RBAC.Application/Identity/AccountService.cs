using Microsoft.AspNetCore.Identity;
using NET.AutoWebApi.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Auth.JwtBearer.Authentication;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Core.Exceptions;
using Yi.Framework.Ddd.Services;
using Yi.RBAC.Application.Contracts.Account.Dtos;
using Yi.RBAC.Domain.Identity;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Identity.Repositories;
using Yi.RBAC.Domain.Shared.Identity.ConstClasses;

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
        private UserManager _userManager { get; set; }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> PostLoginAsync(LoginInputVo input)
        {
            UserEntity user = new();
            //登录成功
            await _userManager.LoginValidationAsync(input.UserName, input.Password, x => user = x);

            //获取用户信息
            var userInfo = await _userRepository.GetUserAllInfoAsync(user.Id);

            //发送令牌
            var claimDic = new Dictionary<string, object>();
            claimDic.Add(nameof(ICurrentUser.Id), 123456);

            var token = _jwtTokenManager.CreateToken(claimDic);
            return token;
        }
    }
}
