
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Helper;

namespace Yi.Framework.Autofac.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            //这里会通过反射,扫码全部程序集获取继承Profile的类
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            var profileList = new List<Type>();
            assemblies.ForEach(a =>
            {
                if (a.FullName is not null)
                {
                    profileList.AddRange(AssemblyHelper.GetClassByParentClass(a.FullName, typeof(Profile)));
                }

            });
            services.AddAutoMapper(profileList.ToArray());
            return services;
        }
    }
}
