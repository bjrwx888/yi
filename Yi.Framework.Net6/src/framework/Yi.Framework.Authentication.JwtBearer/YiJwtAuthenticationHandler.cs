using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Security.Claims;

namespace Yi.Framework.Authentication.JwtBearer
{
    public class YiJwtAuthenticationHandler : IAuthenticationHandler
    {
        public YiJwtAuthenticationHandler()
        {

        }
        public const string YiJwtSchemeName = "YiJwtAuth";

        private AuthenticationScheme _scheme;
        private HttpContext _context;
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            return Task.CompletedTask;
        }


        /// <summary>
        /// 生成认证票据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private AuthenticationTicket GetAuthTicket(string name, string role)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
    new Claim(ClaimTypes.Name, name),
    new Claim(ClaimTypes.Role, role),
            }, YiJwtSchemeName);
            var principal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationTicket(principal, _scheme.Name);
        }

        /// <summary>
        /// 处理操作
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            AuthenticateResult result;
            _context.Request.Headers.TryGetValue("Authorization", out StringValues values);
            string valStr = values.ToString();
            if (!string.IsNullOrWhiteSpace(valStr))
            {
                //认证模拟basic认证：cusAuth YWRtaW46YWRtaW4=
                string[] authVal = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(valStr.Substring(YiJwtSchemeName.Length + 1))).Split(':');
                //var loginInfo = new Dto.LoginDto() { Username = authVal[0], Password = authVal[1] };
                //var validVale = _userService.IsValid(loginInfo);
                bool validVale = true;
                if (!validVale)
                    result = AuthenticateResult.Fail("未登陆");
                else
                {
                    //这里应该将token进行效验，然后加入解析的claim中即可

                    var ticket = GetAuthTicket("cc", "admin");
                    result = AuthenticateResult.Success(ticket);
                }
            }
            else
            {
                result = AuthenticateResult.Fail("未登陆");
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 未登录时的处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ChallengeAsync(AuthenticationProperties? properties)
        {
            _context.Request.Headers.TryGetValue("Authorization", out StringValues values);
            _context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 权限不足的处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ForbidAsync(AuthenticationProperties? properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        }


    }
}