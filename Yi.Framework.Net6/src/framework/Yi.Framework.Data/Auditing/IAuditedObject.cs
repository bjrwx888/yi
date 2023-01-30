using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Data.Auditing
{
    public interface IAuditedObject : ICreationAuditedObject, IModificationAuditedObject
    {
    }

    public interface IAuditedObject<TUser> : IAuditedObject, ICreationAuditedObject<TUser>, IModificationAuditedObject<TUser>
    {

    }

}
