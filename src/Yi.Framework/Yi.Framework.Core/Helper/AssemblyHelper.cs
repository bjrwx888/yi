﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Helper
{
    public static class AssemblyHelper
    {

        public static List<Assembly> GetReferanceAssemblies(this AppDomain domain)
        {
            var list = new List<Assembly>();
            domain.GetAssemblies().ToList().ForEach(i =>
            {
                GetReferanceAssemblies(i, list);
            });
            return list;
        }
        private static void GetReferanceAssemblies(Assembly assembly, List<Assembly> list)
        {
            assembly.GetReferencedAssemblies().ToList().ForEach(i =>
            {
                var ass = Assembly.Load(i);
                if (!list.Contains(ass))
                {
                    list.Add(ass);
                    GetReferanceAssemblies(ass, list);
                }
            });
        }

        public static List<Type> GetClass(string assemblyFile, string? className = null, string? spaceName = null)
        {
            Assembly assembly = Assembly.Load(assemblyFile);
            return assembly.GetTypes().Where(m => m.IsClass
            && className == null ? true : m.Name == className
            && spaceName == null ? true : m.Namespace == spaceName
             ).ToList();
        }

        public static List<Type> GetClassByInterfaces(string assemblyFile, Type type)
        {
            Assembly assembly = Assembly.Load(assemblyFile);

            List<Type> resList = new List<Type>();

            List<Type> typeList = assembly.GetTypes().Where(m => m.IsClass).ToList();
            foreach (var t in typeList)
            {
                var data = t.GetInterfaces();
                if (data.Contains(type))
                {
                    resList.Add(t);
                }

            }
            return resList;
        }

    }
}
