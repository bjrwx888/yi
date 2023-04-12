namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 当前租户接口
/// </summary>
public interface ICurrentTenant
{
    /// <summary>
    /// 是否有效
    /// </summary>
    bool IsAvailable { get; }

    /// <summary>
    /// 租户ID
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// 租户名称
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// 替换租户
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    IDisposable Change(Guid? id, string? name = null);
}
