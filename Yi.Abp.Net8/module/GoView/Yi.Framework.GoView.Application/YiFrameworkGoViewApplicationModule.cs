using Microsoft.AspNetCore.Builder;
using Volo.Abp;
using Volo.Abp.Modularity;
using Yi.Framework.Ddd.Application;
using Yi.Framework.GoView.Application.Contracts;
using Yi.Framework.GoView.Domain;

namespace Yi.Framework.GoView.Application
{
    [DependsOn(typeof(YiFrameworkDddApplicationModule),
        typeof(YiFrameworkGoViewApplicationContractsModule),
        typeof(YiFrameworkGoviewDomainModule))]
    public class YiFrameworkGoViewApplicationModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

            var app = context.GetApplicationBuilder();

            // 添加自定义中间件
            app.UseMiddleware<ResultFormattingMiddleware>();
        }
    }
}
