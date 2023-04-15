using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Data.Auditing
{
    public interface IModificationAuditedObject : IHasModificationTime
    {
        long? LastModifierId { get; }
    }

    public interface IModificationAuditedObject<TUser> : IModificationAuditedObject
    {
        TUser LastModifier { get; }
    }

}
