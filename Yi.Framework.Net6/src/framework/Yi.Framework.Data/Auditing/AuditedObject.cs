using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Data.Auditing
{
    public class AuditedObject : IAuditedObject
    {
        public DateTime CreationTime { get; set; }= DateTime.Now;

        public long? CreatorId { get; set; }

        public long? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
