using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Yi.Framework.Core.Helper;

namespace Yi.Framework.Auth.JwtBearer.Authentication
{
    public class YiJwtAuthenticationHandler : IAuthenticationHandler
    {
        private JwtTokenManager _jwtTokenManager;
        public YiJwtAuthenticationHandler(JwtTokenManager jwtTokenManager)
        {
            _jwtTokenManager = jwtTokenManager;
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
        private AuthenticationTicket GetAuthTicket(IDictionary<string, object> dicClaims)
        {
            List<Claim> claims = new List<Claim>();
            foreach (var claim in dicClaims)
            {
                var p = (JsonElement)claim.Value;
                string? resp=null;
                switch (p.ValueKind)
                {

                    case JsonValueKind.String:
                        resp = p.GetString();
                        break;
                    case JsonValueKind.Number:
                        resp = p.GetInt32().ToString();
                        break;
                }
                claims.Add(new Claim(claim.Key, resp ?? ""));
            }
            var claimsIdentity = new ClaimsIdentity(claims.ToArray(), YiJwtSchemeName);
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
            AuthenticateResult result = AuthenticateResult.Fail("未发现授权令牌");
            _context.Request.Headers.TryGetValue("Authorization", out StringValues values);
            string valStr = values.ToString();
            if (!string.IsNullOrWhiteSpace(valStr))
            {
                var tokenHeader = valStr.Substring(0, 6);
                if (tokenHeader == "Bearer")
                {
                    var token = valStr.Substring(7);

                    var claimDic = _jwtTokenManager.VerifyToken(token, new JwtTokenManager.TokenVerifyErrorAction()
                    {
                        TokenExpiredAction = (ex) => { result = AuthenticateResult.Fail("Token过期"); },
                        SignatureVerificationAction = (ex) => { result = AuthenticateResult.Fail("Token效验失效"); },
                        TokenNotYetValidAction = (ex) => { result = AuthenticateResult.Fail("Token完全错误"); },
                        ErrorAction = (ex) => { result = AuthenticateResult.Fail("Token内部错误"); }
                    });
                    if (claimDic is not null)
                    {
                        //成功
                        result = AuthenticateResult.Success(GetAuthTicket(claimDic));
                    }
                }
                else
                {
                    result = AuthenticateResult.Fail("授权令牌格式错误");
                }

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