using Furion.EventBus;
using IPTools.Core;
using UAParser;
using Yi.Framework.Infrastructure.AspNetCore;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Core.Rbac.Entities;
using Yi.Furion.Core.Rbac.Etos;

namespace Yi.Furion.Application.Rbac.Event
{
    public class LoginEventHandler : IEventSubscriber,ISingleton
    {
        private readonly IRepository<LoginLogEntity> _loginLogRepository;
        public LoginEventHandler(IRepository<LoginLogEntity> loginLogRepository)
        {
            _loginLogRepository = loginLogRepository;
        }
        //[EventSubscribe(nameof(LoginEventSource))]
        public Task HandlerAsync(EventHandlerExecutingContext context)
        {
            var eventData = (LoginEventArgs)context.Source.Payload;
            var loginLogEntity = GetLoginLogInfo(eventData.httpContext);
            loginLogEntity.Id = SnowflakeHelper.NextId;
            loginLogEntity.LogMsg = eventData.UserName + "登录系统";
            loginLogEntity.LoginUser = eventData.UserName;
            loginLogEntity.LoginIp = eventData.httpContext.GetClientIp();

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
