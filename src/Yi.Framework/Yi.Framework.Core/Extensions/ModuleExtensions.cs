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

namespace Yi.Framework.Core.Extensions
{

    /// <summary>
    /// 该类来自于StartupModules
    /// </summary>
    public static class ModuleExtensions
    {
        //
        // 摘要:
        //     Configures startup modules and automatically discovers StartupModules.IStartupModule's
        //     from the applications entry assembly.
        //
        // 参数:
        //   builder:
        //     The Microsoft.AspNetCore.Hosting.IWebHostBuilder instance.
        //
        // 返回结果:
        //     The Microsoft.AspNetCore.Hosting.IWebHostBuilder instance.
        public static IWebHostBuilder UseYiModules(this IWebHostBuilder builder)
        {
            return builder.UseStartupModules(delegate (StartupModulesOptions options)
            {
                options.DiscoverStartupModules();
            });
        }

        //
        // 摘要:
        //     Configures startup modules and automatically discovers StartupModules.IStartupModule's
        //     from the specified assemblies.
        //
        // 参数:
        //   builder:
        //     The Microsoft.AspNetCore.Hosting.IWebHostBuilder instance.
        //
        //   assemblies:
        //     The assemblies to discover startup modules from.
        //
        // 返回结果:
        //     The Microsoft.AspNetCore.Hosting.IWebHostBuilder instance.
        public static IWebHostBuilder UseYiModules(this IWebHostBuilder builder, params Assembly[] assemblies)
        {
            Assembly[] assemblies2 = assemblies;
            return builder.UseStartupModules(delegate (StartupModulesOptions options)
            {
                options.DiscoverStartupModules(assemblies2);
            });
        }

        //
        // 摘要:
        //     Configures startup modules with the specified configuration for StartupModules.StartupModulesOptions.
        //
        // 参数:
        //   builder:
        //     The Microsoft.AspNetCore.Hosting.IWebHostBuilder instance.
        //
        //   configure:
        //     A callback to configure StartupModules.StartupModulesOptions.
        //
        // 返回结果:
        //     The Microsoft.AspNetCore.Hosting.IWebHostBuilder instance.
        public static IWebHostBuilder UseYiModules(this IWebHostBuilder builder, Action<StartupModulesOptions> configure)
        {
            Action<StartupModulesOptions> configure2 = configure;
            return builder.ConfigureServices(delegate (WebHostBuilderContext hostContext, IServiceCollection services)
            {
                services.AddStartupModules(hostContext.Configuration, hostContext.HostingEnvironment, configure2);
            });
        }

        //
        // 摘要:
        //     Configures startup modules with the specified configuration for StartupModules.StartupModulesOptions.
        //
        // 参数:
        //   builder:
        //     The Microsoft.AspNetCore.Builder.WebApplicationBuilder instance.
        //
        // 返回结果:
        //     The Microsoft.AspNetCore.Builder.WebApplicationBuilder instance.
        public static WebApplicationBuilder UseYiModules(this WebApplicationBuilder builder)
        {
            return builder.UseStartupModules(delegate (StartupModulesOptions options)
            {
                options.DiscoverStartupModules();
            });
        }

        //
        // 摘要:
        //     Configures startup modules with the specified configuration for StartupModules.StartupModulesOptions.
        //
        // 参数:
        //   builder:
        //     The Microsoft.AspNetCore.Builder.WebApplicationBuilder instance.
        //
        //   assemblies:
        //     The assemblies to discover startup modules from.
        //
        // 返回结果:
        //     The Microsoft.AspNetCore.Builder.WebApplicationBuilder instance.
        public static WebApplicationBuilder UseYiModules(this WebApplicationBuilder builder, params Assembly[] assemblies)
        {
            Assembly[] assemblies2 = assemblies;
            return builder.UseStartupModules(delegate (StartupModulesOptions options)
            {
                options.DiscoverStartupModules(assemblies2);
            });
        }

        //
        // 摘要:
        //     Configures startup modules with the specified configuration for StartupModules.StartupModulesOptions.
        //
        // 参数:
        //   builder:
        //     The Microsoft.AspNetCore.Builder.WebApplicationBuilder instance.
        //
        //   configure:
        //     A callback to configure StartupModules.StartupModulesOptions.
        //
        // 返回结果:
        //     The Microsoft.AspNetCore.Builder.WebApplicationBuilder instance.
        public static WebApplicationBuilder UseYiModules(this WebApplicationBuilder builder, Action<StartupModulesOptions> configure)
        {
            builder.Services.AddStartupModules(builder.Configuration, builder.Environment, configure);
            return builder;
        }

        //
        // 摘要:
        //     Configures startup modules with the specified configuration for StartupModules.StartupModulesOptions.
        //
        // 参数:
        //   services:
        //     The service collection to add the StartupModules services to.
        //
        //   configuration:
        //     The application's configuration.
        //
        //   environment:
        //     The application's environment information.
        //
        //   configure:
        //     A callback to configure StartupModules.StartupModulesOptions.
        public static void AddYiModules(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment, Action<StartupModulesOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (environment == null)
            {
                throw new ArgumentNullException("environment");
            }

            StartupModulesOptions startupModulesOptions = new StartupModulesOptions();
            configure(startupModulesOptions);
            if (startupModulesOptions.StartupModules.Count != 0 || startupModulesOptions.ApplicationInitializers.Count != 0)
            {
                StartupModuleRunner runner = new StartupModuleRunner(startupModulesOptions);
                services.AddSingleton((Func<IServiceProvider, IStartupFilter>)((IServiceProvider sp) => ActivatorUtilities.CreateInstance<ModulesStartupFilter>(sp, new object[1] { runner })));
                new ConfigureServicesContext(configuration, environment, startupModulesOptions);
                runner.ConfigureServices(services, configuration, environment);
            }
        }
    }
}

