using Yi.Framework.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Yi.Framework.WebCore
{
    public static class CommonExtend
    {
        /// <summary>
        /// 判断是否为异步请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }

        /// <summary>
        /// 基于HttpContext,当前鉴权方式解析，获取用户信息
        /// 现在使用redis作为缓存，不需要将菜单存放至jwt中了
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static UserEntity GetCurrentUserEntityInfo(this HttpContext httpContext, out List<Guid> menuIds)
        {
            IEnumerable<Claim> claimlist = null;
            long resId = 0;
            try
            {

                claimlist = httpContext.AuthenticateAsync().Result.Principal.Claims;
                resId = Convert.ToInt64(claimlist.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sid).Value);

            }
            catch
            {
                throw new Exception("未授权，Token鉴权失败！");
            }



            menuIds = claimlist.Where(u => u.Type == "menuIds").ToList().Select(u => new Guid(u.Value)).ToList();


            return new UserEntity()
            {
                Id = resId,
                //Name = claimlist.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value
            };
        }
    }
}
