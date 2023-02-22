using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using Yi.Framework.Core;
using Yi.Framework.Core.Attributes;

namespace Yi.Framework.OperLogManager
{
    [DependsOn(typeof(YiFrameworkCoreModule))]
    public class YiFrameworkOperLogManagerModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {

            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalOperLogAttribute>();
            });
            //services.AddAutoApiService(opt =>
            //{
            //    //NETServiceTest所在程序集添加进动态api配置
            //    opt.CreateConventional(typeof(YiFrameworkOperLogManagerModule).Assembly, option => option.RootPath = string.Empty);

            //});

            services.AddSingleton<GlobalOperLogAttribute>();
            services.AddTransient<OperationLogService>();
            services.AddTransient<IOperationLogService, OperationLogService>();
        }
    }
}