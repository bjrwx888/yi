using Furion.EventBus;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Furion.Core.Bbs.Entities;
using Yi.Furion.Core.Bbs.Etos;

namespace Yi.Furion.Application.Bbs.Event
{
    public class SeeDiscussEventHandler : IEventSubscriber, ISingleton
    {
        private IRepository<DiscussEntity> _repository;
        public SeeDiscussEventHandler(IRepository<DiscussEntity> repository)
        {
            _repository = repository;
        }
        //[EventSubscribe(nameof(LoginEventSource))]
        public async Task HandlerAsync(EventHandlerExecutingContext context)
        {
            var eventData = (SeeDiscussEventArgs)context.Source.Payload;
            var entity = await _repository.GetByIdAsync(eventData.DiscussId);
            if (entity is not null)
            {
                entity.SeeNum += 1;
                await _repository.UpdateAsync(entity);
            }
        }

    }
}
