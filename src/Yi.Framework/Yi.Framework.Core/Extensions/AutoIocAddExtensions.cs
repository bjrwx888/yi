using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Core.DependencyInjection;

namespace Yi.Framework.Core.Extensions
{
    public static class AutoIocAddExtensions
    {
        public static IServiceCollection AddAutoIocServer(this IServiceCollection services, params string[] assemblyStr)
        {
            foreach (var a in assemblyStr)
            {
                RegIoc(services, Assembly.Load(a));
            }
            return services;
        }

        /// <summary>
        /// 扫描全部
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoIocServer(this IServiceCollection services)
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in assemblys)
            {
                RegIoc(services, a);
            }
            return services;
        }
        private static void RegIoc(IServiceCollection services, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                RegIocByAttribute(services, type);
                RegIocByInterface(services, type);
            }
        }

        private static void RegIocByAttribute(IServiceCollection services, Type type)
        {
            var serviceAttribute = type.GetCustomAttribute<AppServiceAttribute>();
            if (serviceAttribute is not null)
            {
                //泛型类需要单独进行处理
                //情况1：使用自定义[AppService(ServiceType = typeof(注册抽象或者接口))]，手动去注册，放type即可
                var serviceType = serviceAttribute.ServiceType;
                //情况2 自动去找接口，如果存在就是接口，如果不存在就是本身
                if (serviceType == null)
                {
                    //获取最靠近的接口
                    var firstInter = type.GetInterfaces().LastOrDefault();
                    if (firstInter is null)
                    {
                        serviceType = type;
                    }
                    else
                    {
                        serviceType = firstInter;
                    }
                }

                switch (serviceAttribute.ServiceLifetime)
                {
                    case LifeTime.Singleton:
                        services.AddSingleton(serviceType, type);
                        break;
                    case LifeTime.Scoped:
                        services.AddScoped(serviceType, type);
                        break;
                    case LifeTime.Transient:
                        services.AddTransient(serviceType, type);
                        break;
                }

            }
        }

        private static void RegIocByInterface(IServiceCollection services, Type type)
        {
            var serviceInterfaces = type.GetInterfaces();
            if (serviceInterfaces is not null)
            {
                var serviceType = type;
                var firstInter = type.GetInterfaces().Where(u => u != typeof(ITransientDependency)).LastOrDefault();

                if (firstInter is not null)
                {
                    serviceType = firstInter;
                }
                if (serviceInterfaces.Contains(typeof(ITransientDependency)))
                {
                    services.AddTransient(serviceType,type);
                }
                if (serviceInterfaces.Contains(typeof(IScopedDependency)))
                {
                    services.AddScoped(serviceType,type);
                }
                if (serviceInterfaces.Contains(typeof(ISingletonDependency)))
                {
                    services.AddSingleton(serviceType,type);
                }
            }
        }
    }
}
