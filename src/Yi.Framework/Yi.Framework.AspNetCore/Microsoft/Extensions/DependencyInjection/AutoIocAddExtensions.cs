using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attribute;

namespace Microsoft.Extensions.DependencyInjection
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
           var assemblys= AppDomain.CurrentDomain.GetAssemblies();
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
                        var firstInter = type .GetInterfaces().LastOrDefault();
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
        }
    }
}
