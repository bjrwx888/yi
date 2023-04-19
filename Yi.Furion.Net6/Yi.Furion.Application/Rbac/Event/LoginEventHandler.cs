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
        [EventSubscribe(nameof(LoginEventSource))]
        public Task HandlerAsync(EventHandlerExecutingContext context)
        {
            var eventData = (LoginEventArgs)context.Source.Payload;
            var loginLogEntity = eventData.LoginLogEntity;
            loginLogEntity.Id = SnowflakeHelper.NextId;
            loginLogEntity.LogMsg = eventData.UserName + "登录系统";
            loginLogEntity.LoginUser = eventData.UserName;

            _loginLogRepository.InsertAsync(loginLogEntity);
            return Task.CompletedTask;
        }



    
    }
}
