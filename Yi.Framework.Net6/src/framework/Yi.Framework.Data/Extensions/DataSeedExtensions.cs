using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Configuration;
using Yi.Framework.Data.DataSeeds;
using Yi.Framework.Uow;

namespace Yi.Framework.Data.Extensions
{
    public static class DataSeedExtensions
    {
        public static IApplicationBuilder UseDataSeedServer(this IApplicationBuilder builder)
        {
            if (!Appsettings.appBool("EnabledDataSeed"))
            {
                return builder;
            }

            var dataSeeds = builder.ApplicationServices.GetServices<IDataSeed>();
            var iUnitOfWorkManager = builder.ApplicationServices.GetRequiredService<IUnitOfWorkManager>();
            if (dataSeeds is not null)
            {
                using (var uow = iUnitOfWorkManager.CreateContext())
                {
                    foreach (var seed in dataSeeds)
                    {
                        seed.InvokerAsync();
                    }
                    var res = uow.Commit();

                    if (!res)
                    {
                        throw new ApplicationException("种子数据初始化异常");
                    }
                }
            }
            return builder.UseMiddleware<DataFilterMiddleware>();
        }
    }
}
