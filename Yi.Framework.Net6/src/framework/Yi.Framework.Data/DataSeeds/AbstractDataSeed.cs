﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Repositories;

namespace Yi.Framework.Data.DataSeeds
{
    public abstract class AbstractDataSeed<TEntity> : IDataSeed
    {
        private readonly IRepository<TEntity> _repository;
        public AbstractDataSeed(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 简单种子数据，重写该方法即可
        /// </summary>
        /// <returns></returns>
        public abstract List<TEntity> GetSeedData();


        /// <summary>
        /// 复杂数据，重写该方法即可
        /// </summary>
        /// <returns></returns>
        public async virtual Task<bool> DataHandlerAsync()
        {
            return await _repository.InsertRangeAsync(GetSeedData());
        }

        /// <summary>
        /// 这个用来处理判断是否数据库还存在数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> IsInvoker()
        {
            if (await _repository.CountAsync(u => true) > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 完全自定义数据，处理该方法即可
        /// </summary>
        /// <returns></returns>
        public async virtual Task<bool> InvokerAsync()
        {
            bool res = true;
            if (await IsInvoker())
            {
                return await DataHandlerAsync();
            }
            return res;
        }
    }
}