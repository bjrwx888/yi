using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.Caching
{
    /// <summary>
    /// 考虑到本地缓存与分布式缓存差异太大，使用功能限制太大，所以该抽象类淘汰
    /// </summary>
    public abstract class CacheManager
    {

        public virtual bool Exits(string key)
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

        public virtual long Del(string key)
        {
            throw new NotImplementedException();
        }

        public virtual bool HSet(string key, string fieId, object data)
        {
            throw new NotImplementedException();
        }

        public virtual bool HSet(string key, string fieId, object data, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public virtual T HGet<T>(string key, string field)
        {
            throw new NotImplementedException();
        }


        public virtual long HDel(string key, params string[] par)
        {
            throw new NotImplementedException();
        }

        public virtual long HLen(string key)
        {
            throw new NotImplementedException();
        }

        public virtual Dictionary<string, string> HGetAll(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 简单发布
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual long Publish(string channel, string message)
        {
            throw new NotImplementedException();
        }

        public virtual bool LSet(string key, long index, object value)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 列表插入头部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual long LPush<T>(string key, params T[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 列表弹出头部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T LPop<T>(string key)
        {
            throw new NotImplementedException();
        }

        public virtual string[] Keys(string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
