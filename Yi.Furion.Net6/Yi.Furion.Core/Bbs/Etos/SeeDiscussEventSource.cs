using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Furion.EventBus;
using Yi.Furion.Core.Rbac.Etos;

namespace Yi.Furion.Core.Bbs.Etos
{
    public class SeeDiscussEventSource : IEventSource
    {
        public SeeDiscussEventSource(SeeDiscussEventArgs payload)
        { Payload = payload; }
        public string EventId => nameof(SeeDiscussEventSource);
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public CancellationToken CancellationToken { get; set; }


        public object Payload { get; set; }
    }

    public class SeeDiscussEventArgs
    {
        public long DiscussId { get; set; }
        public int OldSeeNum { get; set; }
    }
}
