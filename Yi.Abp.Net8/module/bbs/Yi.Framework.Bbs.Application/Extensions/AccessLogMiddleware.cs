using Microsoft.AspNetCore.Http;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Shared.Caches;
using Yi.Framework.Bbs.Domain.Shared.Enums;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Application.Extensions;

/// <summary>
/// 访问日志中间件
/// 并发最高，采用缓存，默认10分钟才会真正操作一次数据库
///
/// 需考虑一致性问题，又不能上锁影响性能
/// </summary>
public class AccessLogMiddleware : IMiddleware, ITransientDependency
{
    private readonly IDistributedCache<AccessLogCacheItem> _accessLogCache;
    private readonly ISqlSugarRepository<AccessLogAggregateRoot> _repository;

    public AccessLogMiddleware(IDistributedCache<AccessLogCacheItem> accessLogCache,
        ISqlSugarRepository<AccessLogAggregateRoot> repository)
    {
        _accessLogCache = accessLogCache;
        _repository = repository;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);

        //获取缓存
        var cache = await _accessLogCache.GetAsync(AccessLogCacheConst.Key);

        //当前缓存需赋的值
        long currentNumber = 1;
        //最后插入的时间
        DateTime lastInsertTime = DateTime.Now;
        //cahce是空，创建缓存，当前数量从数据库中获取
        //不等于空，如果大于当前天，插入数据库（间隔超过10分钟，也插入）

        //获取当前访问数量
        //没有缓存
        if (cache is null)
        {
            //获取数据库今天最后最后一条数据
            var currentDayEntity = await GetDateAccessLogEntityAsync();

            //数据库有数据，以数据库数据为准+1
            if (currentDayEntity is not null)
            {
                currentNumber = currentDayEntity.Number + 1;
            }
            //数据库没有数据，就是默认的1，重新开始
        }
        else
        {
            //有缓存，根据根据缓存的值来
            currentNumber = cache.Number + 1;
            lastInsertTime = cache.LastInsertTime;

            //数据库持久化
            //缓存的日期大于当天的日期，插入到数据库,同时缓存变成0，重新开始统计
            if (cache.LastModificationTime.Date > DateTime.Today)
            {
                await _repository.InsertAsync(new AccessLogAggregateRoot()
                    { AccessLogType = AccessLogTypeEnum.Request, Number = currentNumber });
                currentNumber = 0;
            }
            else
            {
                if (cache.LastInsertTime <= DateTime.Now - TimeSpan.FromMinutes(10))
                {
                    //这里还需要判断数据库是否有值，不能之间更新
                    
                    //缓存的时间大于当前数据库时间10分钟之后，更新（减少数据库交互，10分钟才更新一次）
                    var currentDayEntity = await GetDateAccessLogEntityAsync();
                    if (currentDayEntity is null)
                    {
                        await _repository.InsertAsync(new AccessLogAggregateRoot()
                            { AccessLogType = AccessLogTypeEnum.Request, Number = currentNumber });
                    }
                    await _repository.UpdateAsync(currentDayEntity);
                }
            }
        }


        //覆盖缓存
        var newCache = new AccessLogCacheItem(currentNumber) { LastInsertTime = lastInsertTime };
        await _accessLogCache.SetAsync(AccessLogCacheConst.Key, newCache);
    }

    /// <summary>
    /// 获取今天的统计
    /// </summary>
    /// <returns></returns>
    private async Task<AccessLogAggregateRoot?> GetDateAccessLogEntityAsync()
    {
        //获取数据库今天最后最后一条数据
        var currentDayEntity = await _repository._DbQueryable
            .Where(x => x.AccessLogType == AccessLogTypeEnum.Request)
            .Where(x => x.CreationTime == DateTime.Today)
            .OrderByDescending(x => x.CreationTime)
            .FirstAsync();
        return currentDayEntity;
    }
}