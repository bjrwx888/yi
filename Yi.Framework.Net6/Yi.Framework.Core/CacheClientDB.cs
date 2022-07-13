using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Yi.Framework.Common.IOCOptions;
using CSRedis;

namespace Yi.Framework.Core
{
    public class CacheClientDB
    {

        public delegate T MyAction<T>(CSRedisClient client);

        private readonly RedisConnOptions _RedisOptions;
        public CacheClientDB(IOptionsMonitor<RedisConnOptions> redisConnOptions)
        {
            this._RedisOptions = redisConnOptions.CurrentValue;
        }
        //public CSRedisClient GetClient()
        //{
        //    return client;
        //}
        //private CSRedisClient client=null;

        // 为了以后全链路做准备

        private T TryCatch<T>(MyAction<T> action)
        {
            //Stopwatch sw = Stopwatch.StartNew();
            ////Exception ex = null;
            ////bool isError = false;
            var client2 = new CSRedisClient($"{_RedisOptions.Host}:{_RedisOptions.Prot},password={_RedisOptions.Password},defaultDatabase ={ _RedisOptions.DB }");
            T result;
            try
            {
                result = action(client2);
            }
            catch (Exception exinfo)
            {
                object p = null;
                result = (T)p;
                //isError = true;
                Console.WriteLine(exinfo);

            }
            finally
            {
                client2.Dispose();
            }

            return result;
        }


        public bool Exit(string key)
        {
            return this.TryCatch((u) => u.Exists(key));
        }

        public long Remove(string key)
        {
            return this.TryCatch((u) => u.Del(key));
        }

        public long HRemove(string key, params string[] par)
        {
            return this.TryCatch((u) => u.HDel(key, par));
        }

        public T Get<T>(string key)
        {
            return this.TryCatch<T>((u) => u.Get<T>(key));
        }
        public bool Set<T>(string key, T data, TimeSpan time)
        {
            return this.TryCatch<bool>((u) => u.Set(key, data, time));
        }

        public bool Set<T>(string key, T data)
        {
            return this.TryCatch<bool>((u) => u.Set(key, data));
        }
        public T QueuePop<T>(string key)
        {
            return this.TryCatch<T>((u) => u.RPop<T>(key));
        }
        public long QueuePush<T>(string key, T data)
        {
            return this.TryCatch<long>((u) => u.LPush<T>(key, data));
        }
        public long QueueLen(string key)
        {
            return TryCatch((u) => u.LLen(key));
        }

        public bool HSet<T>(string key, string fieId, T data)
        {
            return this.TryCatch<bool>((u) => u.HSet(key, fieId, data));
        }
        public bool HSet<T>(string key, string fieId, T data, TimeSpan time)
        {
            return this.TryCatch<bool>((u) =>
            {
                var res = u.HSet(key, fieId, data);
                u.Expire(key, time);
                return res;
            });
        }

        public CSRedisClient Db()
        {
            return new CSRedisClient($"{_RedisOptions.Host}:{_RedisOptions.Prot},password={_RedisOptions.Password},defaultDatabase ={ _RedisOptions.DB }");
        }
    }
}
