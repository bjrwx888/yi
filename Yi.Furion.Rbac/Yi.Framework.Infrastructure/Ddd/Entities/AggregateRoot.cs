using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Ddd.Entities
{
    public class AggregateRoot : IEntity, IAggregateRoot
    {
    }
    public class AggregateRoot<TKey> : Entity<TKey>, IEntity<TKey>
    {
    }
}
