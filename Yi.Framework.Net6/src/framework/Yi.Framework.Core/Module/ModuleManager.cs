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
        /// <summary>
        /// 全部程序集
        /// </summary>
        private List<Type> ResultType = new List<Type>();

        /// <summary>
        /// 开始程序集
        /// </summary>
        private Type StartType;
        public ModuleManager(Type startType)
        {
            StartType = startType;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public List<Type> Invoker()
        {
            StartBFSNodes(StartType);
            ResultType= ResultType.Distinct().ToList();
            var result = StartTopologicalSortNodes().Reverse().ToList();

            Logger? _logger = LogManager.Setup().LoadConfigurationFromAssemblyResource(typeof(ModuleManager).Assembly).GetCurrentClassLogger();
            foreach (var r in result)
            {
                //添加全局模块程序集
                ModuleAssembly.Add(r.Assembly);
                _logger.Info($"意框架正在加载模块:{r.Name}");
            }
            return result;
        }


        private Type[] StartTopologicalSortNodes()
        {
            List<TopologicalSortNode<Type>> topologicalSortNodes = new List<TopologicalSortNode<Type>>();


            //添加注册到节点
            foreach (var res in ResultType)
            {
                var typeNode = new TopologicalSortNode<Type>(res);
                topologicalSortNodes.Add(typeNode);
            }

            Dictionary<Type, TopologicalSortNode<Type>> nodeDic = topologicalSortNodes.ToDictionary(x => x.Data);


            //各个节点互相添加依赖
            foreach (var node in topologicalSortNodes)
            {
                GetDependsOnType(node.Data)?.ToList().ForEach(x => node.AddDependent(nodeDic[x]));
            }

          

            return TopologicalSortNode<Type>.TopologicalSort(topologicalSortNodes).Select(x => x.Data).ToArray();
        }

        /// <summary>
        /// BFS获取全部程序集
        /// </summary>
        /// <param name="node"></param>
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


        /// <summary>
        /// 获取模块 需依赖的模块
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Type[]? GetDependsOnType(Type type)
        {
            var dependsOnbuild = type.GetCustomAttributes(typeof(DependsOnAttribute), false).FirstOrDefault() as DependsOnAttribute;
            if (dependsOnbuild is null)
            {
                return new Type[0];
            }
            return dependsOnbuild.GetDependedTypes();
        }


        public List<Assembly> ToAssemblyList()
        {
            return ResultType.Select(a => a.Assembly).ToList();
        }
        public Assembly[] ToAssemblyArray(List<Type> types)
        {
            return types.Select(a => a.Assembly).ToArray();
        }
    }
}
