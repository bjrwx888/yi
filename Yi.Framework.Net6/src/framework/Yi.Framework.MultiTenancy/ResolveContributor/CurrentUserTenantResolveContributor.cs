using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.Core.CurrentUsers;

namespace Yi.Framework.MultiTenancy.ResolveContributor;

/// <summary>
/// 当前用户中获取租户
/// </summary>
public class CurrentUserTenantResolveContributor : TenantResolveContributorBase
{
    /// <summary>
    /// 贡献者名称
    /// </summary>
    public const string ContributorName = "CurrentUser";

    /// <summary>
    /// 贡献者名称
    /// </summary>
    public override string Name => ContributorName;

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="context">解析器上下文</param>
    /// <returns></returns>
    public override Task ResolveAsync(ITenantResolveContext context)
    {
        ICurrentUser currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
        if (currentUser.TenantId != Guid.Empty)
        {
            context.Handled = true;
            context.TenantIdOrName = currentUser.TenantId.ToString();
        }
        else
        {
            context.Handled = false;
        }
        return Task.CompletedTask;
    }
}