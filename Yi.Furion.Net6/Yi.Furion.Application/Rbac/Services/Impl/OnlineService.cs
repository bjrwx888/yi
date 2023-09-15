using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Furion.Application.Rbac.SignalRHub;
using Yi.Furion.Application.Rbac.SignalRHub.Model;

namespace Yi.Furion.Application.Rbac.Services.Impl
{
    [ApiDescriptionSettings("RBAC")]
    public class OnlineService:IOnlineService,IDynamicApiController,ITransient
    {
        private ILogger<OnlineService> _logger;
        private IHubContext<OnlineUserHub> _hub;
        public OnlineService(ILogger<OnlineService> logger, IHubContext<OnlineUserHub> hub)
        {
            _logger = logger;
            _hub = hub;
        }

        /// <summary>
        /// 动态条件获取当前在线用户
        /// </summary>
        /// <param name="online"></param>
        /// <returns></returns>
        [HttpGet("")]
        public PagedResultDto<OnlineUserModel> GetListAsync([FromQuery] OnlineUserModel online)
        {
            var data = OnlineUserHub.clientUsers;
            IEnumerable<OnlineUserModel> dataWhere = data.AsEnumerable();

            if (!string.IsNullOrEmpty(online.Ipaddr))
            {
                dataWhere = dataWhere.Where((u) => u.Ipaddr!.Contains(online.Ipaddr));
            }
            if (!string.IsNullOrEmpty(online.UserName))
            {
                dataWhere = dataWhere.Where((u) => u.UserName!.Contains(online.UserName));
            }
            return new PagedResultDto<OnlineUserModel>() { Total = data.Count, Items = dataWhere.ToList() };
        }


        /// <summary>
        /// 强制退出用户
        /// </summary>
        /// <param name="connnectionId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{connnectionId}")]
        public async Task<bool> ForceOut(string connnectionId)
        {
            if (OnlineUserHub.clientUsers.Exists(u => u.ConnnectionId == connnectionId))
            {
                //前端接受到这个事件后，触发前端自动退出
                await _hub.Clients.Client(connnectionId).SendAsync("forceOut", "你已被强制退出！");
                return true;
            }
            return false;
        }
    }
}
