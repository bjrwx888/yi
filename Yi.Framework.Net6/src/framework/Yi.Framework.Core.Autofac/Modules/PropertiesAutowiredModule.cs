using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;


namespace Yi.Framework.Core.Autofac.Modules
{
    internal class PropertiesAutowiredModule : YiAutoFacModule
    {
        internal override void Load(ContainerBuilder containerBuilder, params Assembly[] assemblies)
        {
            containerBuilder.RegisterAssemblyTypes(assemblies)
                .PropertiesAutowired(new AutowiredPropertySelector());
        }

    }

    public class AutowiredPropertySelector : IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            return propertyInfo.CustomAttributes.Any(it => it.AttributeType == typeof(AutowiredAttribute));
        }
    }

}
