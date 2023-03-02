using AspNetCore.Microsoft.AspNetCore.Hosting;
using Yi.Framework.Core.Autofac.Extensions;
using Yi.Framework.Core.Autofac.Modules;
using Yi.Framework.Core.Extensions;
using Yi.BBS.Web;
using Yi.Framework.Core.Module;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStartUrlsServer(builder.Configuration);

builder.UseYiModules(typeof(YiBBSWebModule));

//添加autofac模块,需要添加模块
builder.Host.ConfigureAutoFacContainer(container =>
{
    container.RegisterYiModule(AutoFacModuleEnum.PropertiesAutowiredModule, ModuleAssembly.Assemblies);
});

var app = builder.Build();

app.UseErrorHandlingServer();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
