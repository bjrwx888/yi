using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.Data.Entities;
using Yi.Framework.MultiTenancy;

namespace Yi.Framework.MultiTenancy.ResolveContributor;

/// <summary>
/// <see cref="IConfiguration"/>租户解析器贡献者
/// </summary>
public class ConfigurationTenantResolveContributor : TenantResolveContributorBase
{
    /// <summary>
    /// 租户解析器贡献者基类
    /// </summary>
    public override string Name => "Configuration";

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="context">解析器上下文</param>
    /// <returns></returns>
    public override Task ResolveAsync(ITenantResolveContext context)
    {
        IConfiguration? configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();

        string? tenantIdStr = configuration.GetValue<string>(nameof(IMultiTenant.TenantId));

        if (Guid.TryParse(tenantIdStr, out Guid tenantId))
        {
            if (tenantId != Guid.Empty)
            {
                context.TenantIdOrName = tenantId.ToString();
                context.Handled = true;
            }
        }

        context.Handled = false;
        return Task.CompletedTask;
    }
}
