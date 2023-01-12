using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Attribute
{
    /// 1、[AppService]：自动去找接口，如果存在就是接口，如果不存在就是本身
    /// 2、[AppService(ServiceType = typeof(注册抽象或者接口或者本身))]，手动去注册，放type即可
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AppServiceAttribute : System.Attribute
    {
        /// <summary>
        /// 服务声明周期
        /// 不给默认值的话注册的是作用域
        /// </summary>
        public LifeTime ServiceLifetime { get; set; } = LifeTime.Transient;
        /// <summary>
        /// 指定服务类型
        /// </summary>
        public Type? ServiceType { get; set; }

    }

    public enum LifeTime
    {
        Transient, Scoped, Singleton
    }
}
