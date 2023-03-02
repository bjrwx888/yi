namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 租户信息
/// </summary>
public class BasicTenantInfo
{
    /// <summary>
    /// 租户ID
    /// </summary>
    public Guid? TenantId { get; }

    /// <summary>
    /// 租户ID 名称
    /// </summary>
    public string Name { get; }

    /// <summary/>
    public BasicTenantInfo(Guid? tenantId, string? name = null)
    {
        TenantId = tenantId;
        Name = name ?? string.Empty;
    }
}
