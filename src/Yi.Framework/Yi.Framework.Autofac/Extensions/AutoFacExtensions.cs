using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Autofac.Extensions
{
    public static class AutoFacExtensions
    {
        public static IHostBuilder UseAutoFacServerProviderFactory(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            return hostBuilder;
        }

    }
}
