using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Auth.JwtBearer.Authentication.Options;
using Yi.Framework.Core.Helper;

namespace Yi.Framework.Auth.JwtBearer.Authentication
{
    public class JwtTokenManager
    {
        private JwtTokenOptions _jwtTokenOptions;

        public JwtTokenManager(IOptions<JwtTokenOptions> options)
        {
            _jwtTokenOptions = options.Value;
        }
        public string CreateToken(Dictionary<string, object>? claimDic)
        {
            var token = JwtBuilder.Create()
                          .WithAlgorithm(new RS256Algorithm(RSAFileHelper.GetKey(), RSAFileHelper.GetKey()))
                          .AddClaim(ClaimName.Issuer, _jwtTokenOptions.Issuer)
                          .AddClaim(ClaimName.Audience, _jwtTokenOptions.Audience)
                          .AddClaim(ClaimName.Subject, _jwtTokenOptions.Subject)
                          .AddClaim(ClaimName.IssuedAt, UnixEpoch.GetSecondsSince(new DateTimeOffset(DateTime.UtcNow)))
                          .ExpirationTime(DateTime.Now.AddSeconds(_jwtTokenOptions.ExpSecond));
            if (claimDic is not null)
            {
                foreach (var d in claimDic)
                {
                    token.AddClaim(d.Key, d.Value);
                };
            }
            return token.Encode();
        }

        public IDictionary<string, object>? VerifyToken(string token, TokenVerifyErrorAction tokenVerifyErrorAction)
        {
            IDictionary<string, object>? claimDic = null;
            try
            {

                claimDic = JwtBuilder.Create()
                       .WithAlgorithm(new RS256Algorithm(RSAFileHelper.GetPublicKey()))
                      .WithValidationParameters(ValidationParameters.Default)
                       .Decode<IDictionary<string, object>>(token);
            }
            catch (TokenNotYetValidException ex)
            {
                if (tokenVerifyErrorAction.TokenNotYetValidAction is not null)
                {
                    tokenVerifyErrorAction.TokenNotYetValidAction(ex);
                }
                //Console.WriteLine("Token错误");
            }
            catch (TokenExpiredException ex)
            {
                if (tokenVerifyErrorAction.TokenExpiredAction is not null)
                {
                    tokenVerifyErrorAction.TokenExpiredAction(ex);
                }
                //Console.WriteLine("Token过期");
            }
            catch (SignatureVerificationException ex)
            {
                if (tokenVerifyErrorAction.SignatureVerificationAction is not null)
                {
                    tokenVerifyErrorAction.SignatureVerificationAction(ex);
                }
                //Console.WriteLine("Token无效");
            }
            catch (Exception ex)
            {
                if (tokenVerifyErrorAction.ErrorAction is not null)
                {
                    tokenVerifyErrorAction.ErrorAction(ex);
                }
                //Console.WriteLine("Token内部错误，json序列化");
            }
            return claimDic;

        }

        public class TokenVerifyErrorAction
        {
            public Action<TokenNotYetValidException>? TokenNotYetValidAction { get; set; }

            public Action<TokenExpiredException>? TokenExpiredAction { get; set; }
            public Action<SignatureVerificationException>? SignatureVerificationAction { get; set; }

            public Action<Exception>? ErrorAction { get; set; }
        }
    }
}
