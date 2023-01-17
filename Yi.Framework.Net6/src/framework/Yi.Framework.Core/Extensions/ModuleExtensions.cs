using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupModules.Internal;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Module;

namespace Yi.Framework.Core.Extensions
{

    /// <summary>
    /// 该类来自于StartupModules
    /// </summary>
    public static class ModuleExtensions
    {
        public static WebApplicationBuilder UseYiModules(this WebApplicationBuilder builder, Type startType)
        {
            var moduleManager = new ModuleManager(startType);
            moduleManager.Invoker();
           
            Assembly[] assemblies2 = moduleManager.ToAssemblyArray();
            return builder.UseStartupModules(delegate (StartupModulesOptions options)
            {
                options.DiscoverStartupModules(assemblies2);
            });
        }
    }
}

