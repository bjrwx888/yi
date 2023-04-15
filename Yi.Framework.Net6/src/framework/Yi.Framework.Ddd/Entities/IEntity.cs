using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Entities
{
    public interface IEntity
    {
        //
        // 摘要:
        //     Returns an array of ordered keys for this entity.

    }
    public interface IEntity<TKey> : IEntity
    {
        //
        // 摘要:
        //     Unique identifier for this entity.
        TKey Id { get; set; }
    }
}
