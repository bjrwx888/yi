using AspNetCore.Microsoft.AspNetCore.Hosting;
using Yi.Framework.Authentication.JwtBearer;
using Yi.Framework.Core.Autofac.Extensions;
using Yi.Framework.Core.Autofac.Modules;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Web;

TimeTest.Start();
var builder = WebApplication.CreateBuilder(args);

//设置启动url
builder.WebHost.UseStartUrlsServer(builder.Configuration);

//添加模块
builder.UseYiModules(typeof(YiFrameworkWebModule));

builder.Services.AddAuthentication(YiJwtAuthenticationHandler.YiJwtSchemeName);

builder.Services.AddAuthentication(option =>
{
    option.AddScheme<YiJwtAuthenticationHandler>(YiJwtAuthenticationHandler.YiJwtSchemeName, YiJwtAuthenticationHandler.YiJwtSchemeName);
});
//添加autofac模块,需要添加模块
builder.Host.ConfigureAutoFacContainer(container =>
{
    container.RegisterYiModule(AutoFacModuleEnum.PropertiesAutowiredModule, typeof(YiFrameworkWebModule).Assembly);
});

var app = builder.Build();

var t = app.Services.GetService<Test2Entity>();

//全局错误中间件，需要放在最早
app.UseErrorHandlingServer();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
