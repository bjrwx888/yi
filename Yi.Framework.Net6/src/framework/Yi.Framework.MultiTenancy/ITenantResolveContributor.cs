namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 租户解析器贡献者
/// </summary>
public interface ITenantResolveContributor
{
    /// <summary>
    /// 贡献者名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="context">解析器上下文</param>
    /// <returns></returns>
    Task ResolveAsync(ITenantResolveContext context);
}
