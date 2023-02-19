using Cike.EventBus.EventHandlerAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.RBAC.Domain.Shared.Identity.Etos;

namespace Yi.RBAC.Domain.Identity.Event
{
    public class LoginEventHandler : IDistributedEventHandler<LoginEventArgs>
    {
        public Task HandlerAsync(LoginEventArgs eventData)
        {
            Console.WriteLine(eventData.UserName+"登录系统");
            return Task.CompletedTask;
        }
    }
}
