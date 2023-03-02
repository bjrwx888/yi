namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 解析器上下文
/// </summary>
public class TenantResolveContext : ITenantResolveContext
{
    /// <summary/>
    public TenantResolveContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    /// <summary/>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 租户ID 或 名称
    /// </summary>
    public string TenantIdOrName { get; set; }

    /// <summary>
    /// 是否已处理
    /// </summary>
    public bool Handled { get; set; }

    /// <summary>
    /// 是否已经处理或者<see cref="TenantIdOrName"/>为有效值
    /// </summary>
    public bool HasResolvedTenantOrHost()
    {
        return Handled
            || TenantIdOrName != Guid.Empty.ToString() && !string.IsNullOrWhiteSpace(TenantIdOrName);
    }
}
