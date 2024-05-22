using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Yi.Framework.Bbs.Domain.Entities;

namespace Yi.Framework.Bbs.Domain.EventHandlers
{
    public class BbsNoticeCreatedEventHandler : ILocalEventHandler<EntityCreatedEventData<BbsNoticeAggregateRoot>>,
      ITransientDependency
    {
        public Task HandleEventAsync(EntityCreatedEventData<BbsNoticeAggregateRoot> eventData)
        {
            throw new NotImplementedException();
        }
    }
}
