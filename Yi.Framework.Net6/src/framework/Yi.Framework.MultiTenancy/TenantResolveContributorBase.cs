namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 租户解析器贡献者基类
/// </summary>
public abstract class TenantResolveContributorBase : ITenantResolveContributor
{
    /// <summary>
    /// 贡献者名称
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="context">解析器上下文</param>
    /// <returns></returns>
    public abstract Task ResolveAsync(ITenantResolveContext context);
}