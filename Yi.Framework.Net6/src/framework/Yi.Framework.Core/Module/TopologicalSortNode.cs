using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Module
{
    public class TopologicalSortNode<T>
    {
        public T Data { get; private set; }
        public List<TopologicalSortNode<T>> Dependents { get; private set; }
        public int IncomingEdges { get; private set; }
        public TopologicalSortNode(T data)
        {
            Data = data;
            Dependents = new List<TopologicalSortNode<T>>();
            IncomingEdges = 0;
        }
        public void AddDependent(TopologicalSortNode<T> dependent)
        {
            Dependents.Add(dependent);
            dependent.IncomingEdges++;
        }


        public static List<TopologicalSortNode<T>> TopologicalSort(List<TopologicalSortNode<T>> graph)
        {
            List<TopologicalSortNode<T>> result = new List<TopologicalSortNode<T>>();
            Queue<TopologicalSortNode<T>> queue = new Queue<TopologicalSortNode<T>>();
            // 将所有入度为 0 的节点加入队列
            foreach (TopologicalSortNode<T> node in graph)
            {
                if (node.IncomingEdges == 0)
                {
                    queue.Enqueue(node);
                }
            }
            // 依次将入度为 0 的节点出队，并将它的依赖节点的入度减 1
            while (queue.Count > 0)
            {
                TopologicalSortNode<T> node = queue.Dequeue();
                result.Add(node);
                foreach (TopologicalSortNode<T> dependent in node.Dependents)
                {
                    dependent.IncomingEdges--;
                    if (dependent.IncomingEdges == 0)
                    {
                        queue.Enqueue(dependent);
                    }
                }
            }
            // 如果存在入度不为 0 的节点，则说明图中存在环
            foreach (TopologicalSortNode<T> node in graph)
            {
                if (node.IncomingEdges != 0)
                {
                    throw new ArgumentException("模块之间存在互相依赖！");
                }
            }
            return result;
        }


    }
}
