using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Module
{
    public class ModuleAssembly
    {
        private static HashSet<Assembly> assemblies=new HashSet<Assembly>();
        public static Assembly[] Assemblies { get=> assemblies.ToArray();}

        public static void Add(Assembly assembly)
        {
            assemblies.Add(assembly);
        }
    }
}
