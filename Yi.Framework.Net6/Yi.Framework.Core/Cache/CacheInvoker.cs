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
    public abstract class CacheInvoker
    {

        private readonly RedisConnOptions _RedisOptions;

        protected CacheInvoker Client { get; set; }

        public CacheInvoker Db { get { return Client; } set { } }
        public CacheInvoker(IOptionsMonitor<RedisConnOptions> redisConnOptions)
        {

        }
        public virtual bool Exit(string key)
        {
            throw new NotImplementedException();
        }

        public virtual long Remove(string key)
        {
            throw new NotImplementedException();
        }

        public virtual long HRemove(string key, params string[] par)
        {
            throw new NotImplementedException();
        }
        public virtual T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public virtual bool Set<T>(string key, T data, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public virtual bool Set<T>(string key, T data)
        {
            throw new NotImplementedException();
        }
        public virtual T QueuePop<T>(string key)
        {
            throw new NotImplementedException();
        }
        public virtual long QueuePush<T>(string key, T data)
        {
            throw new NotImplementedException();
        }
        public virtual long QueueLen(string key)
        {
            throw new NotImplementedException();
        }

        public virtual bool HSet<T>(string key, string fieId, T data)
        {
            throw new NotImplementedException();
        }
        public virtual bool HSet<T>(string key, string fieId, T data, TimeSpan time)
        {
            throw new NotImplementedException();
        }

    }
}
