using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Yi.Framework.Infrastructure.Data.DataSeeds
{
    public static class DataSeedExtensions
    {
        public static async Task<IApplicationBuilder> UseDataSeedServer(this IApplicationBuilder builder)
        {
            if (!App.Configuration.GetSection("EnabledDataSeed").Get<bool>())
            {
                return builder;
            }

            var dataSeeds = builder.ApplicationServices.GetServices<IDataSeed>();
            var iUnitOfWorkManager = builder.ApplicationServices.GetRequiredService<ISqlSugarClient>();
            if (dataSeeds is not null)
            {
                //using (var uow = iUnitOfWorkManager.CreateContext())
                //{
                var res = await iUnitOfWorkManager.Ado.UseTranAsync(async () =>
                    {
                        foreach (var seed in dataSeeds)
                        {
                            await seed.InvokerAsync();
                        }
                    });

                //var res = uow.Commit();

                if (!res.IsSuccess)
                {
                    throw new ApplicationException("种子数据初始化异常");
                }
                //}
            }
            return builder.UseMiddleware<DataFilterMiddleware>();
        }
    }
}
