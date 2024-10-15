using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Services;
using Volo.Abp.Settings;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Consts;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Dtos;
using Yi.Framework.SettingManagement.Domain;

namespace Yi.Framework.DigitalCollectibles.Domain.Managers;

/// <summary>
/// 矿池领域服务
/// 处理矿池相关业务，例如挖矿等
/// </summary>
public class MiningPoolManager : DomainService
{
    /// <summary>
    /// 每次挖矿概率，每天根据特定算法计算
    /// </summary>
    private static decimal CurrentMiningProbability { get; set; }

    private readonly ISettingProvider _settingProvider;
    private readonly IDistributedCache<MiningPoolContent> _cache;

    public MiningPoolManager(ISettingProvider settingProvider, IDistributedCache<MiningPoolContent> cache)
    {
        _settingProvider = settingProvider;
        this._cache = cache;
    }

    /// <summary>
    /// 最后一次计算挖矿概率的时间，如果时间跨了早上10点，重新计算
    /// </summary>
    private static DateTime LastComputeMiningProbabilityTime { get; set; }

    /// <summary>
    /// 用户进行一次挖矿
    /// </summary>
    /// <returns></returns>
    public Task MiningAsync(Guid userId)
    {
        //从矿池中开挖
        //根据挖矿概率，进行挖掘
        //挖到了放到用户仓库即可

        //如果概率是挖到了矿，再从矿物中随机选择一个稀有度，再在当前稀有度中的矿物列表，随机选择一个具体的矿物

        throw new NotImplementedException();
    }

    /// <summary>
    /// 计算当前挖矿概率
    /// </summary>
    /// <returns></returns>
    public Task ComputeMiningProbabilityAsync()
    {
        //当前的挖矿期望：当天的所有藏品能被刚好挖完
        
        //保底概率：最大不能高过一个值，百分之10
        //手动挖矿：1天可挖10次，每次至少间隔6秒
        //自动挖矿：1天可以挖24次  每次间隔60分钟（需要使用自动挖矿卡）
        //可影响因素：剩余手动挖矿次数+剩余自动挖矿次数
        
        //所有能够挖掘次数
        throw new NotImplementedException();
    }

    /// <summary>
    /// 刷新矿池
    /// </summary>
    /// <returns></returns>
    public async Task RefreshMiningPoolAsync()
    {
        //获取当前最大的限制
        var maximumPoolLimit = int.Parse(await _settingProvider.GetOrNullAsync("MaximumPoolLimit"));

        DateTime startTime = DateTime.Today.AddHours(10);
        DateTime endTime = startTime.AddDays(1);
        var probabilityValues = RarityEnumExtensions.GetProbabilityValues();
        var result = GenerateDistribution(maximumPoolLimit, probabilityValues);

        //根据配置，将不同比例的矿，塞入矿池,
        //矿池，交给redis
        await _cache.SetAsync(CacheConst.MiningPoolContent, new MiningPoolContent(startTime, endTime)
        {
            I0_OrdinaryNumber = result[0],
            I1_SeniorNumber = result[1],
            I2_RareNumber = result[2],
            I3_GemNumber = result[3],
            I4_LegendNumber = result[4]
        }, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = endTime
        });
    }

    /// <summary>
    /// 根据概率生成给对应稀有度藏品
    /// </summary>
    /// <param name="totalCount"></param>
    /// <param name="probabilities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private int[] GenerateDistribution(int totalCount, decimal[] probabilities)
    {
        int[] counts = new int[probabilities.Length];
        decimal totalProbability = 0;

        // 计算概率总和，确保为 1
        foreach (var prob in probabilities)
        {
            totalProbability += prob;
        }

        // 检查概率之和是否为 1
        if (totalProbability < 0.99m || totalProbability > 1.01m)
        {
            throw new ArgumentException("概率总和必须接近1");
        }

        // 生成分布
        for (int i = 0; i < probabilities.Length; i++)
        {
            counts[i] = (int)(totalCount * probabilities[i]);
        }

        // 处理可能因精度问题导致的总数不足
        int sum = 0;
        foreach (var count in counts)
        {
            sum += count;
        }

        int difference = totalCount - sum;

        // 将差值分配给概率最大的一项
        if (difference > 0)
        {
            int maxIndex = Array.IndexOf(counts, Math.Max(counts[0], counts[1]));
            counts[maxIndex] += difference;
        }

        return counts;
    }
}