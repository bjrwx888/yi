using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Furion.EventBus;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Core.Rbac.Etos
{
    public class LoginEventSource : IEventSource
    {
        public LoginEventSource(LoginEventArgs payload)
        { Payload = payload; }
        public string EventId => nameof(LoginEventSource);
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public CancellationToken CancellationToken { get; set; }


        public object Payload { get; set; }
    }

    public class LoginEventArgs
    {
        public long UserId { get; set; }
        public string UserName { get; set; }

        public LoginLogEntity LoginLogEntity { get; set; }
    }
}
