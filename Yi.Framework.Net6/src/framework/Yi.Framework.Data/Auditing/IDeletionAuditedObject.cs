using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Data.Auditing
{
    public interface IDeletionAuditedObject : IHasDeletionTime
    {
        long? DeleterId { get; }
    }

    public interface IDeletionAuditedObject<TUser> : IDeletionAuditedObject
    {

        TUser Deleter { get; }
    }
}
