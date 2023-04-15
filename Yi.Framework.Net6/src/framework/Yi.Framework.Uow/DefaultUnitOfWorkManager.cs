using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Uow
{
    internal class DefaultUnitOfWorkManager : IUnitOfWorkManager
    {
        public IUnitOfWork CreateContext(bool isTran = true)
        {
            return new DefaultUnitOfWork();
        }
    }
}
