using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Repositories;

namespace Yi.Framework.Uow
{
    internal class DefaultUnitOfWork : IUnitOfWork
    {
        public DefaultUnitOfWork() { }
        public bool IsTran { get; set; }
        public bool IsCommit { get; set; }
        public bool IsClose { get; set; }

        public bool Commit()
        {
            return true;
        }

        public void Dispose()
        {
        }

        public IRepository<T> GetRepository<T>()
        {
            throw new NotImplementedException();
        }
    }
}
