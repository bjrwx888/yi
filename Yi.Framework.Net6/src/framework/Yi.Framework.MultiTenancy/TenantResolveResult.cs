namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 租户解析器结果
/// </summary>
public class TenantResolveResult
{
    /// <summary>
    /// 租户ID 或 名称
    /// </summary>
    public string TenantIdOrName { get; set; } = Guid.Empty.ToString();

    /// <summary>
    /// 存储遍历过的<see cref="ITenantResolveContributor.Name"/>
    /// </summary>
    public List<string> AppliedResolvers { get; } = new List<string>();
}
