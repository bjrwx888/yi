using Yi.Framework.Core.Utils;

namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 当前租户实现
/// </summary>
public class CurrentTenant : ICurrentTenant
{
    private readonly ICurrentTenantAccessor _currentTenantAccessor;

    /// <summary/>
    public CurrentTenant(ICurrentTenantAccessor currentTenantAccessor)
    {
        _currentTenantAccessor = currentTenantAccessor;
    }

    /// <summary>
    /// 是否有效
    /// </summary>
    public virtual bool IsAvailable => Id != Guid.Empty;

    /// <summary>
    /// 租户ID
    /// </summary>
    public virtual Guid Id => _currentTenantAccessor.Current?.TenantId ?? Guid.Empty;

    /// <summary>
    /// 租户名称
    /// </summary>
    public string? Name => _currentTenantAccessor.Current?.Name;

    /// <summary>
    /// 替换租户
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public IDisposable Change(Guid? id, string? name = null)
    {
        return SetCurrent(id, name);
    }

    /// <summary>
    /// 设置当前租户
    /// </summary>
    /// <returns></returns>
    private IDisposable SetCurrent(Guid? tenantId, string? name = null)
    {
        BasicTenantInfo? parentScope = _currentTenantAccessor.Current;
        _currentTenantAccessor.Current = new BasicTenantInfo(tenantId, name);

        return new DisposeAction<ValueTuple<ICurrentTenantAccessor, BasicTenantInfo>>(static ((ICurrentTenantAccessor, BasicTenantInfo) state) =>
        {
            (ICurrentTenantAccessor currentTenantAccessor, BasicTenantInfo parentScope) = state;
            currentTenantAccessor.Current = parentScope;
        },
        (_currentTenantAccessor, parentScope));
    }
}
