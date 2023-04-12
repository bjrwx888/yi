using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Yi.Framework.MultiTenancy;

/// <summary>
/// 
/// </summary>
public class TenantResolver : ITenantResolver
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TenantResolveOptions _options;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TenantResolver(IOptions<TenantResolveOptions> options, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _options = options.Value;
    }

    /// <summary>
    /// 解析租户Id或名称
    /// </summary>
    public virtual async Task<TenantResolveResult> ResolveTenantIdOrNameAsync()
    {
        TenantResolveResult? result = new TenantResolveResult();

        using (IServiceScope? serviceScope = _serviceProvider.CreateScope())
        {
            TenantResolveContext? context = new TenantResolveContext(serviceScope.ServiceProvider);

            foreach (ITenantResolveContributor tenantResolver in _options.TenantResolvers)
            {
                await tenantResolver.ResolveAsync(context);

                result.AppliedResolvers.Add(tenantResolver.Name);

                if (context.HasResolvedTenantOrHost())
                {
                    result.TenantIdOrName = context.TenantIdOrName;
                    break;
                }
            }
        }
        return result;
    }
}
