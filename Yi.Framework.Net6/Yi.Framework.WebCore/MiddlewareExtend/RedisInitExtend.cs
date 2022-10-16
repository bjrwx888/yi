using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Core;

namespace Yi.Framework.WebCore.MiddlewareExtend
{
  public static class RedisInitExtend
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RedisInitExtend));
        public  static  void UseRedisSeedInitService(this IApplicationBuilder app )
        {

            if (Appsettings.appBool("RedisSeed_Enabled"))
            {
                var _cacheClientDB = app.ApplicationServices.GetService<CacheInvoker>();

                try
                {
                    //RedisInit.Seed(_cacheClientDB);
                }
                catch (Exception e)
                {
                    log.Error($"Error occured seeding the RedisInit.\n{e.Message}");
                    throw;
                }
            }
        }
    }
}
