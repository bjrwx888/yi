using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.DataSeeds;

namespace Yi.Framework.Data.Extensions
{
    public static class DataSeedExtensions
    {
        public static IApplicationBuilder UseDataSeedServer(this IApplicationBuilder builder)
        {
            var dataSeeds = builder.ApplicationServices.GetServices(typeof(IDataSeed<>));
            //这个种子数据是有问题的，先放个坑

            return builder.UseMiddleware<DataFilterMiddleware>();
        }
    }
}
