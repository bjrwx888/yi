using Cike.EventBus.EventHandlerAbstracts;
using IPTools.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UAParser;
using Yi.Framework.AspNetCore.Extensions;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Logs.Entities;
using Yi.RBAC.Domain.Shared.Identity.Etos;

namespace Yi.RBAC.Domain.Logs.Event
{
    public class LoginEventHandler : IDistributedEventHandler<LoginEventArgs>
    {
        private readonly IRepository<LoginLogEntity> _loginLogRepository;
        private readonly HttpContext _httpContext;
        public LoginEventHandler(IRepository<LoginLogEntity> loginLogRepository, IHttpContextAccessor httpContextAccessor)
        {
            _loginLogRepository = loginLogRepository;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public Task HandlerAsync(LoginEventArgs eventData)
        {
            var loginLogEntity = GetLoginLogInfo(_httpContext);
            loginLogEntity.Id = SnowflakeHelper.NextId;
            loginLogEntity.LogMsg = eventData.UserName + "登录系统";
            loginLogEntity.LoginUser = eventData.UserName;
            loginLogEntity.LoginIp = _httpContext.GetClientIp();

            _loginLogRepository.InsertAsync(loginLogEntity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取客户端信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static ClientInfo GetClientInfo(HttpContext context)
        {
            var str = context.GetUserAgent();
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(str);
            return c;
        }

        /// <summary>
        /// 记录用户登陆信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static LoginLogEntity GetLoginLogInfo(HttpContext context)
        {
            var ipAddr = context.GetClientIp();
            IpInfo location;
            if (ipAddr == "127.0.0.1")
            {
                location = new IpInfo() { Province = "本地", City = "本机" };
            }
            else
            {
                location = IpTool.Search(ipAddr);
            }
            ClientInfo clientInfo = GetClientInfo(context);
            LoginLogEntity entity = new()
            {
                Browser = clientInfo.Device.Family,
                Os = clientInfo.OS.ToString(),
                LoginIp = ipAddr,
                LoginLocation = location.Province + "-" + location.City
            };

            return entity;
        }
    }
}
