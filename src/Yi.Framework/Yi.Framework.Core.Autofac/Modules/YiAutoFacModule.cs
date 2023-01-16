using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Autofac.Modules
{
    public abstract class YiAutoFacModule
    {
        internal abstract void Load(ContainerBuilder containerBuilder,params Assembly[] assemblies);
    }
}
