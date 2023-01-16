using Autofac;
using Autofac.Core.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Autofac.Modules;

namespace Yi.Framework.Core.Autofac.Extensions
{
    public static class AutoFacModuleExtensions
    {
        public static void RegisterYiModule<TModule>(this ContainerBuilder builder, params Assembly[] assemblies) where TModule : YiAutoFacModule, new()
        {
            new TModule().Load(builder, assemblies);
        }


        public static void RegisterYiModule(this ContainerBuilder builder, AutoFacModuleEnum autoFacModuleEnum, params Assembly[] assemblies)
        {
            switch (autoFacModuleEnum)
            {
                case AutoFacModuleEnum.PropertiesAutowiredModule:
                    new PropertiesAutowiredModule().Load(builder, assemblies);
                    break;
            }
        }

    }
}
