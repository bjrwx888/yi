using Volo.Abp.Auditing;

namespace Yi.Framework.Ddd.Application.Contracts;

public interface IBackUser : IAuditedObject
{
    /// <summary>
    /// 创建的用户名
    /// </summary>
    public string CreateUserNikeName { get; set; }

    /// <summary>
    /// 更新的用户名
    /// </summary>
    public string UpdateUserNikeName { get; set; }
}