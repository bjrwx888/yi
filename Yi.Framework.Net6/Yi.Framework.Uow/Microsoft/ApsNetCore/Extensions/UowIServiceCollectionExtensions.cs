using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Uow;
using Yi.Framework.Uow.Interceptors;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UowIServiceCollectionExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddSingleton<UnitOfWorkInterceptor>();
        }
    }
}
