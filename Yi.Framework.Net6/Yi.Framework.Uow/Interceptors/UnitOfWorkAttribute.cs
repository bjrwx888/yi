using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Uow.Interceptors
{
    public class UnitOfWorkAttribute : Attribute// : AbstractInterceptorAttribute
    {
        public UnitOfWorkAttribute(bool isTransactional = true)
        {
            IsTransactional = isTransactional;
        }
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, bool isTransactional = true) : this(isTransactional)
        {
            IsolationLevel = isolationLevel;
        }
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout, bool isTransactional = true) : this(isolationLevel, isTransactional)
        {
            Timeout = timeout;
        }

        public bool IsTransactional { get; }

        public IsolationLevel? IsolationLevel { get; }

        /// <summary>
        /// Milliseconds
        /// </summary>
        public int? Timeout { get; }
        public bool IsDisabled { get; }


        //public override Task Invoke(AspectContext context, AspectDelegate next)
        //{
        //    if (IsTransactional)
        //    {
        //        ServiceLocator.in.getservice()
        //    }
        //}
    }
}
