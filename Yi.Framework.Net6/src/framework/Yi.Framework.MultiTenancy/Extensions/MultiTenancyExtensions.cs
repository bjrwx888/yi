using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.MultiTenancy.ResolveContributor;

namespace Yi.Framework.MultiTenancy.Extensions;

/// <summary>
/// 租户注入扩展方法
/// </summary>
public static class MultiTenancyExtensions
{
    /// <summary>
    /// 注入 租户
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCurrentTenant(this IServiceCollection services)
    {
        services.Configure<TenantResolveOptions>(option =>
        {
            //添加租户解析器，默认添加从当前用户中获取

            //添加从httpheader，解析TenantId配置的租户id
            option.TenantResolvers.Add(new HttpHeaderTenantResolveContributor());

            //添加配置租户解析器，解析TenantId配置的租户id
            option.TenantResolvers.Add(new ConfigurationTenantResolveContributor());
        });

        //添加租户解析器注入
        services.AddTransient<ITenantResolver, TenantResolver>();

        //添加当前租户
        services.AddTransient<ICurrentTenant, CurrentTenant>();

        //添加默认访问器
        services.AddTransient<ICurrentTenantAccessor, DefaultCurrentTenantAccessor>();

        return services;


    }
}
