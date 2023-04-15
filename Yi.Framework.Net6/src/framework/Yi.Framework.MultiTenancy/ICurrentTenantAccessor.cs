namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 租户访问器
/// </summary>
public interface ICurrentTenantAccessor
{
    /// <summary>
    /// 当前租户信息
    /// </summary>
    BasicTenantInfo Current { get; set; }
}
