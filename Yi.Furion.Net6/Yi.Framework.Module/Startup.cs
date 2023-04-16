using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yi.Framework.Infrastructure.AspNetCore;
using Yi.Framework.Infrastructure.Sqlsugar;
using Yi.Framework.Module.ImageSharp.HeiCaptcha;
using Yi.Framework.Module.Sms.Aliyun;

namespace Yi.Framework.Module;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCurrentUserServer();

        services.Configure<DbConnOptions>(App.Configuration.GetSection("DbConnOptions"));

        services.AddDbSqlsugarContextServer();

        services.AddHeiCaptcha();

        services.Configure<SmsAliyunOptions>(App.Configuration.GetSection("SmsAliyunOptions"));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

    }
}
