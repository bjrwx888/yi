using AspNetCore.Microsoft.AspNetCore.Hosting;
using NLog;
using NLog.Extensions.Logging;
using Yi.Framework.Core.Autofac.Extensions;
using Yi.Framework.Core.Autofac.Modules;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Core.Module;
using Yi.Template.Web;


var builder = WebApplication.CreateBuilder(args);
//配置日志
builder.Services.AddLogging(builder => { builder.ClearProviders().AddNLog("nlog.config").SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); });
Logger? _logger = LogManager.Setup().LoadConfigurationFromAssemblyResource(typeof(Program).Assembly).GetCurrentClassLogger();
_logger.Info("-----( ¯ □ ¯ )YiFrameowrk框架启动-----");
//设置启动url
builder.WebHost.UseStartUrlsServer(builder.Configuration);

//添加模块
builder.UseYiModules(typeof(YiTemplateWebModule));

//添加autofac模块,需要添加模块
builder.Host.ConfigureAutoFacContainer(container =>
{
    container.RegisterYiModule(AutoFacModuleEnum.PropertiesAutowiredModule, ModuleAssembly.Assemblies);
});

var app = builder.Build();

//全局错误中间件，需要放在最早
app.UseErrorHandlingServer();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();