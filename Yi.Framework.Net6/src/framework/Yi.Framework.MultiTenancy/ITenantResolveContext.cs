namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 解析器上下文
/// （作用于各个<see cref="ITenantResolveContributor.ResolveAsync(ITenantResolveContext)"/>）之间
/// </summary>
public interface ITenantResolveContext
{
    /// <summary/>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 租户ID 或 名称
    /// </summary>
    string TenantIdOrName { get; set; }

    /// <summary>
    /// 是否已处理
    /// </summary>
    bool Handled { get; set; }
}
