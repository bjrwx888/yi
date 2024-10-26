using System.Globalization;
using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;
using Yi.Abp.Application;
using Yi.Abp.SqlsugarCore;
using Yi.Framework.AspNetCore;
using Yi.Framework.AspNetCore.Authentication.OAuth;
using Yi.Framework.AspNetCore.Authentication.OAuth.Gitee;
using Yi.Framework.AspNetCore.Authentication.OAuth.QQ;
using Yi.Framework.AspNetCore.Microsoft.AspNetCore.Builder;
using Yi.Framework.AspNetCore.Microsoft.Extensions.DependencyInjection;
using Yi.Framework.AspNetCore.UnifyResult;
using Yi.Framework.Bbs.Application;
using Yi.Framework.Bbs.Application.Extensions;
using Yi.Framework.ChatHub.Application;
using Yi.Framework.CodeGen.Application;
using Yi.Framework.Rbac.Application;
using Yi.Framework.Rbac.Domain.Authorization;
using Yi.Framework.Rbac.Domain.Shared.Consts;
using Yi.Framework.Rbac.Domain.Shared.Options;
using Yi.Framework.TenantManagement.Application;

namespace Yi.Abp.Web
{
    [DependsOn(
        // typeof(YiAbpSqlSugarCoreModule),
        // typeof(YiAbpApplicationModule),
         // typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreSerilogModule),
        // typeof(AbpAuditingModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(YiFrameworkAspNetCoreModule),
        typeof(YiFrameworkAspNetCoreAuthenticationOAuthModule)
    )]
    public class YiAbpWebModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var host = context.Services.GetHostingEnvironment();
            var service = context.Services;



            //动态Api
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(YiAbpWebModule).Assembly,
                    options => options.RemoteServiceName = "default");
    

                //统一前缀
                options.ConventionalControllers.ConventionalControllerSettings.ForEach(x => x.RootPath = "api/app");
            });

            //设置api格式
            // service.AddControllers().AddNewtonsoftJson(options =>
            // {
            //     options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            // });

   

            // Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = false; });

            //Swagger
            context.Services.AddYiSwaggerGen<YiAbpWebModule>(options =>
            {
                options.SwaggerDoc("default",
                    new OpenApiInfo { Title = "Yi.Framework.Abp", Version = "v1", Description = "集大成者" });
            });

     

          
            return Task.CompletedTask;
        }


        public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            var service = context.ServiceProvider;

            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();

            app.UseRouting();

            //跨域
            app.UseCors(DefaultCorsPolicyName);

       


            app.UseAuthentication();

    

            //swagger
            app.UseYiSwagger();

   

            //静态资源
            app.UseStaticFiles("/api/app/wwwroot");
            app.UseDefaultFiles();
            app.UseDirectoryBrowser("/api/app/wwwroot");


            //工作单元
            app.UseUnitOfWork();

            //授权
            app.UseAuthorization();


            //日志记录
            app.UseAbpSerilogEnrichers();

            //终节点
            app.UseConfiguredEndpoints();

            return Task.CompletedTask;
        }
    }
}