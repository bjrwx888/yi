using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using Yi.Framework.Infrastructure.AspNetCore;
using Yi.Framework.Infrastructure.Sqlsugar;
using Yi.Framework.Module.Caching;
using Yi.Framework.Module.ImageSharp.HeiCaptcha;
using Yi.Framework.Module.Sms.Aliyun;
using Yi.Framework.Module.WeChat;

namespace Yi.Framework.Module;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHeiCaptcha();

        services.Configure<SmsAliyunOptions>(App.Configuration.GetSection("SmsAliyunOptions"));

        services.Configure<CachingConnOptions>(App.Configuration.GetSection("CachingConnOptions"));

        services.Configure<WeChatOptions>(App.Configuration.GetSection("WeChatOptions"));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
       var db= app.ApplicationServices.GetRequiredService<ISqlSugarClient>();
    }
}
