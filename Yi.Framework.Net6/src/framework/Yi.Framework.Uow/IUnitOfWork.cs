using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsTran { get; set; }
        bool IsCommit { get; set; }
        bool IsClose { get; set; }

        bool Commit();
    }
}
