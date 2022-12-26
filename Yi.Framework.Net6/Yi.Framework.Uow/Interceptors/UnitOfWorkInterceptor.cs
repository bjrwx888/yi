using Castle.DynamicProxy;
using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Uow.Interceptors
{
    public class UnitOfWorkInterceptor : IInterceptor
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkInterceptor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!IsUnitOfWorkMethod(invocation.Method, out var uowAttr))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                _unitOfWork.BeginTran();
                //执行被拦截的方法
                invocation.Proceed();
                _unitOfWork.CommitTran();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTran();
                throw ex;
            }
        }


        public static bool IsUnitOfWorkMethod( MethodInfo methodInfo,out UnitOfWorkAttribute unitOfWorkAttribute)
        {

            //Method declaration
            var attrs = methodInfo.GetCustomAttributes(true).OfType<UnitOfWorkAttribute>().ToArray();
            if (attrs.Any())
            {
                unitOfWorkAttribute = attrs.First();
                return !unitOfWorkAttribute.IsDisabled;
            }

            if (methodInfo.DeclaringType != null)
            {
                //Class declaration
                attrs = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<UnitOfWorkAttribute>().ToArray();
                if (attrs.Any())
                {
                    unitOfWorkAttribute = attrs.First();
                    return !unitOfWorkAttribute.IsDisabled;
                }

                ////Conventional classes
                //if (typeof(IUnitOfWorkEnabled).GetTypeInfo().IsAssignableFrom(methodInfo.DeclaringType))
                //{
                //    unitOfWorkAttribute = null;
                //    return true;
                //}
            }

            unitOfWorkAttribute = null;
            return false;
        }

    }
}
