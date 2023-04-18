using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPTools.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using UAParser;
using Yi.Framework.Infrastructure.AspNetCore;
using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Furion.Application.Rbac.SignalRHub.Model;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Application.Rbac.SignalRHub
{
    public class OnlineUserHub : Hub
    {
        public static readonly List<OnlineUserModel> clientUsers = new();


        private HttpContext _httpContext;
        private ILogger<OnlineUserHub> _logger;
        private ICurrentUser _currentUser;
        public OnlineUserHub(IHttpContextAccessor httpContextAccessor, ILogger<OnlineUserHub> logger, ICurrentUser currentUser)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _logger = logger;
            _currentUser = currentUser;
        }



        /// <summary>
        /// 成功连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            var name = _currentUser.UserName;
            var loginUser = GetLoginLogInfo(_httpContext);
            var user = clientUsers.Any(u => u.ConnnectionId == Context.ConnectionId);
            //判断用户是否存在，否则添加集合
            if (!user )
            {
                OnlineUserModel users = new(Context.ConnectionId)
                {
                    Browser = loginUser?.Browser,
                    LoginLocation = loginUser?.LoginLocation,
                    Ipaddr = loginUser?.LoginIp,
                    LoginTime = DateTime.Now,
                    Os = loginUser?.Os,
                    UserName = name ?? ""
                };
                clientUsers.Add(users);
                _logger.LogInformation($"{DateTime.Now}：{name},{Context.ConnectionId}连接服务端success，当前已连接{clientUsers.Count}个");

                //Clients.All.SendAsync(HubsConstant.MoreNotice, SendNotice());
            }
            //当有人加入，向全部客户端发送当前总数
            Clients.All.SendAsync("onlineNum", clientUsers.Count);
            //Clients.All.SendAsync(HubsConstant.OnlineUser, clientUsers);
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = clientUsers.Where(p => p.ConnnectionId == Context.ConnectionId).FirstOrDefault();
            //判断用户是否存在，否则添加集合
            if (user != null)
            {
                clientUsers.Remove(user);
                Clients.All.SendAsync("onlineNum", clientUsers.Count);
                //Clients.All.SendAsync(HubsConstant.OnlineUser, clientUsers);
                _logger.LogInformation($"用户{user?.UserName}离开了，当前已连接{clientUsers.Count}个");
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendAllTest(string test)
        {
            await Clients.All.SendAsync("ReceiveAllInfo", test);
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