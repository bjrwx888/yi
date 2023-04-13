using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Ddd.Entities
{
    public interface IAggregateRoot : IEntity
    {
    }
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {
    }
}
