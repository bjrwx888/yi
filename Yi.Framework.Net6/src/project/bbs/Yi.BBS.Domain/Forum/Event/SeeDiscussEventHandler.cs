using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cike.EventBus.EventHandlerAbstracts;
using Yi.BBS.Domain.Forum.Entities;
using Yi.BBS.Domain.Shared.Forum.Etos;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Shared.Identity.Etos;

namespace Yi.BBS.Domain.Forum.Event
{
    public class SeeDiscussEventHandler : IDistributedEventHandler<SeeDiscussEventArgs>
    {
        private IRepository<DiscussEntity> _repository;
        public SeeDiscussEventHandler(IRepository<DiscussEntity> repository)
        {
            _repository = repository;
        }
        public async Task HandlerAsync(SeeDiscussEventArgs eventData)
        {
           var entity= await _repository.GetByIdAsync(eventData.DiscussId);
            if (entity is not null) {
                entity.SeeNum += 1;
                await _repository.UpdateAsync(entity);
            }
   
        }
    }
}
