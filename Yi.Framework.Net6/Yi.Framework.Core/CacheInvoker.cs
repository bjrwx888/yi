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
    public class CacheInvoker
    {

        public delegate T MyAction<T>(CSRedisClient client);

        private readonly RedisConnOptions _RedisOptions;

        private CSRedisClient Client { get; set; }

        public CSRedisClient _Db { get { return Client; } set { } }
        public CacheInvoker(IOptionsMonitor<RedisConnOptions> redisConnOptions)
        {
            this._RedisOptions = redisConnOptions.CurrentValue;
            Client = new CSRedisClient($"{_RedisOptions.Host}:{_RedisOptions.Prot},password={_RedisOptions.Password},defaultDatabase ={ _RedisOptions.DB }");
        }

        private T TryCatch<T>(MyAction<T> action)
        {


            T result = default(T);
            try
            {
                result = action(Client);
            }
            catch (Exception exinfo)
            {
                Console.WriteLine(exinfo);
            }
            //finally
            //{
            //    Client.Dispose();
            //}

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
