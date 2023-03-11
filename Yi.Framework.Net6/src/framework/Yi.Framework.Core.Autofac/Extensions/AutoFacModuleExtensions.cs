using Autofac;
using Autofac.Core.Registration;
using NLog;
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
            Logger? _logger = LogManager.Setup().LoadConfigurationFromAssemblyResource(typeof(AutoFacModuleExtensions).Assembly).GetCurrentClassLogger();
            switch (autoFacModuleEnum)
            {
                case AutoFacModuleEnum.PropertiesAutowiredModule:
                    _logger.Info($"意框架添加AutoFac模块：{nameof(PropertiesAutowiredModule)}-属性注入模块");
                    new PropertiesAutowiredModule().Load(builder, assemblies);
                    break;
            }
        }

    }
}
