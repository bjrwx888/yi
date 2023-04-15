using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Data.Auditing
{
    public interface IFullAuditedObject : IAuditedObject, IDeletionAuditedObject
    {

    }

    public interface IFullAuditedObject<TUser> : IAuditedObject<TUser>, IFullAuditedObject, IDeletionAuditedObject<TUser>
    {

    }

}
