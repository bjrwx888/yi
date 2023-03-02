

using Yi.Framework.MultiTenancy.ResolveContributor;

namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 解析器属性
/// </summary>
public class TenantResolveOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TenantResolveOptions()
    {
        TenantResolvers = new List<ITenantResolveContributor>
        {
            new CurrentUserTenantResolveContributor()
        };
    }

    /// <summary>
    /// 解析器贡献者。由这帮东西为框架提供 租户ID
    /// </summary>
    public List<ITenantResolveContributor> TenantResolvers { get; }
}
