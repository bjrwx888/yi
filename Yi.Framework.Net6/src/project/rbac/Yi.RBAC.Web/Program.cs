using AspNetCore.Microsoft.AspNetCore.Hosting;
using Yi.Framework.Core.Autofac.Extensions;
using Yi.Framework.Core.Autofac.Modules;
using Yi.Framework.Core.Extensions;
using Yi.RBAC.Web;

var builder = WebApplication.CreateBuilder(args);
//设置启动url
builder.WebHost.UseStartUrlsServer(builder.Configuration);

//添加模块
builder.UseYiModules(typeof(YiRBACWebModule));

//添加autofac模块,需要添加模块
builder.Host.ConfigureAutoFacContainer(container =>
{
    container.RegisterYiModule(AutoFacModuleEnum.PropertiesAutowiredModule, typeof(YiRBACWebModule).Assembly);
});

var app = builder.Build();

//全局错误中间件，需要放在最早
app.UseErrorHandlingServer();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
