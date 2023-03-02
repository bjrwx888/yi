namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 默认租户访问器实现
/// </summary>
public class DefaultCurrentTenantAccessor : ICurrentTenantAccessor
{
    private ITenantResolver _tenantResolver;

    /// <summary/>
    public DefaultCurrentTenantAccessor(ITenantResolver tenantResolver)
    {
        _tenantResolver = tenantResolver;
        TenantResolveResult? tenantResolveResult = _tenantResolver.ResolveTenantIdOrNameAsync().Result;
        string? tenantIdStr = tenantResolveResult.TenantIdOrName;

        Current = Guid.TryParse(tenantIdStr, out Guid tehnantId)
            ? new BasicTenantInfo(tehnantId)
            : new BasicTenantInfo(Guid.Empty);
    }

    /// <summary>
    /// 当前租户信息
    /// </summary>
    public BasicTenantInfo Current { get; set; }

}

