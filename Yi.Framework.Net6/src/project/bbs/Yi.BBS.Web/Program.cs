using AspNetCore.Microsoft.AspNetCore.Hosting;
using Yi.Framework.Core.Autofac.Extensions;
using Yi.Framework.Core.Autofac.Modules;
using Yi.Framework.Core.Extensions;
using Yi.BBS.Web;
using Yi.BBS.Application;
using Yi.RBAC.Application;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStartUrlsServer(builder.Configuration);

builder.UseYiModules(typeof(YiBBSWebModule));

builder.Host.ConfigureAutoFacContainer(container =>
{
    container.RegisterYiModule(AutoFacModuleEnum.PropertiesAutowiredModule,
        typeof(YiBBSApplicationModule).Assembly, 
        typeof(YiRBACApplicationModule).Assembly);
});

var app = builder.Build();

app.UseErrorHandlingServer();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
