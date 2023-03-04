using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Attributes;

namespace Yi.Framework.Core.Module
{

    internal class ModuleManager
    {
        private List<Type> ResultType = new List<Type>();
        private Type StartType;
        public ModuleManager(Type startType)
        {
            StartType = startType;
        }

        public List<Type> Invoker()
        {
            StartBFSNodes(StartType);
           var result= RemoveDuplicate(ResultType);
            Logger? _logger = LogManager.Setup().LoadConfigurationFromAssemblyResource(typeof(ModuleManager).Assembly).GetCurrentClassLogger();
            foreach (var r in result)
            {
                //添加全局模块程序集
                ModuleAssembly.Add(r.Assembly);
                _logger.Info($"意框架正在加载模块:{r.Name}");
            }
            return result;
        }

        private Type[]? GetDependsOnType(Type type)
        {
            var dependsOnbuild = type.GetCustomAttributes(typeof(DependsOnAttribute), false).FirstOrDefault() as DependsOnAttribute;
            if (dependsOnbuild is null)
            {
                return new Type[0];
            }
            return dependsOnbuild.GetDependedTypes();

        }

        private void StartBFSNodes(Type node)
        {
            ResultType.Add(node);
            var nodes = GetDependsOnType(node);
            if (nodes is not null && nodes.Count() != 0)
            {
                foreach (var n in nodes)
                {
                    StartBFSNodes(n);
                }
            }
        }

        private List<Type> RemoveDuplicate(List<Type> array)
        {
            HashSet<Type> s = new HashSet<Type>();
            List<Type> list = new List<Type>();
            for (int i = array.Count - 1; i >= 0; i--)
            {
                if (!s.Contains(array[i]))
                {
                    s.Add(array[i]);
                    list.Add(array[i]);
                }
            }
            ResultType = list;
            return list;
        }

        public List<Assembly> ToAssemblyList()
        {
            return ResultType.Select(a => a.Assembly).ToList();
        }
        public Assembly[] ToAssemblyArray()
        {
            return ResultType.Select(a => a.Assembly).ToArray();
        }
    }
}
