using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Repositories;

namespace Yi.Framework.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsTran { get; set; }
        bool IsCommit { get; set; }
        bool IsClose { get; set; }

        IRepository<T> GetRepository<T>();
        bool Commit();
    }
}
