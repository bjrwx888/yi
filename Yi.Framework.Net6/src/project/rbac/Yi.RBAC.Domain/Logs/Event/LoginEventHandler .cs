using Cike.EventBus.EventHandlerAbstracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            var loginLogEntity = new LoginLogEntity();
            loginLogEntity.Id = SnowflakeHelper.NextId;
            loginLogEntity.LogMsg = eventData.LogMsg;
            loginLogEntity.LoginUser = eventData.UserName;

            _loginLogRepository.InsertAsync(loginLogEntity);
            Console.WriteLine(eventData.UserName + "登录系统");
            return Task.CompletedTask;
        }
    }
}
