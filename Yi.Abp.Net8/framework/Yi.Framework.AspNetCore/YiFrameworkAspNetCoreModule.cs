using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;
using Yi.Framework.AspNetCore.Mvc;
using Yi.Framework.Core;

namespace Yi.Framework.AspNetCore
{
    [DependsOn(typeof(YiFrameworkCoreModule)
        )]
    public class YiFrameworkAspNetCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}