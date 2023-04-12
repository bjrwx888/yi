namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 租户解析器接口
/// </summary>
public interface ITenantResolver
{
    /// <summary>
    /// 解析租户Id或名称
    /// </summary>
    /// <returns></returns>
    Task<TenantResolveResult> ResolveTenantIdOrNameAsync();
}
