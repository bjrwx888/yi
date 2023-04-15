using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Data.Auditing
{
    public interface ICreationAuditedObject : IHasCreationTime, IMayHaveCreator
    {

    }

    public interface ICreationAuditedObject<TCreator> : ICreationAuditedObject, IMayHaveCreator<TCreator>
    {

    }

}
