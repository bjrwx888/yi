
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.Data.Entities;
using Yi.Framework.MultiTenancy;

namespace Yi.Framework.MultiTenancy.ResolveContributor;

/// <summary>
/// <see cref="HttpContext"/>租户解析器贡献者
/// </summary>
public class HttpHeaderTenantResolveContributor : TenantResolveContributorBase
{
    /// <summary>
    /// 贡献者名称
    /// </summary>
    public override string Name => "HttpHeader";

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="context">解析器上下文</param>
    /// <returns></returns>
    public override Task ResolveAsync(ITenantResolveContext context)
    {
        IHttpContextAccessor? httpContextAccessor = context.ServiceProvider.GetService<IHttpContextAccessor>();

        //如果没有注入http对象，直接跳出
        if (httpContextAccessor is null)
        {
            return Task.CompletedTask;
        }
        HttpContext? httpContext = httpContextAccessor.HttpContext;
        if (httpContext is not null)
        {
            string? tenantId = httpContext.Request.Headers
                .Where(x => x.Key == nameof(IMultiTenant.TenantId))
                .Select(x => x.Value.ToString())
                .FirstOrDefault();

            if (tenantId is not null)
            {
                if (Guid.TryParse(tenantId, out Guid tid))
                {
                    context.TenantIdOrName = tid.ToString();
                    context.Handled = true;
                }
            }
        }
        return Task.CompletedTask;
    }
}
