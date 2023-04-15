using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Data.Entities
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
