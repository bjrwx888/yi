using System;
using Furion;
using Furion.Schedule;
using Furion.TimeCrontab;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Yi.Framework.Infrastructure.Data.Json;
using Yi.Furion.Application.Rbac.Job;
using Yi.Furion.Application.Rbac.SignalRHub;
using Yi.Furion.Web.Core.Handlers;

namespace Yi.Furion.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConsoleFormatter();
        services.AddJwt<JwtHandler>();

        services.AddCorsAccessor();

        services.AddControllers().AddInjectWithUnifyResult().AddJsonOptions(x => {
            x.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            x.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
        });

        services.AddEventBus();

        services.AddHttpContextAccessor();
        services.AddSignalR();

        services.AddSchedule(options =>
        {
            // 注册作业，并配置作业触发器
            //options.AddJob<TestJob>(Triggers.Period(10000));
            options.AddJob<SystemDataJob>(Triggers.Cron("0 0 0,12 ? * ?",CronStringFormat.WithSeconds)); // 表示每天凌晨与12点
        });
        services.AddFileLogging("log/application-{0:yyyy}-{0:MM}-{0:dd}.log", options =>
        {
            options.Append = true;
            options.MinimumLevel = LogLevel.Information;
            options.FileSizeLimitBytes = 1024 * 1024 * 10;
            options.MaxRollingFiles = 100;
            options.FileNameRule = fileName =>
            {
                return string.Format(fileName, DateTime.UtcNow);
            };
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseInject(string.Empty);
      
    }
}

[AppStartup(-1)]
public class EndStartup : AppStartup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<OnlineUserHub>("/api/hub/main");
            endpoints.MapControllers();
        });
    }
}