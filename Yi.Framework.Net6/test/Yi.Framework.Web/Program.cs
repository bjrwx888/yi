using Yi.Framework.Core.Autofac.Extensions;
using Yi.Framework.Core.Autofac.Modules;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Web;

TimeTest.Start();
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls(builder.Configuration.GetValue<string>("StartUrl"));

//添加模块
builder.UseYiModules(typeof(YiFrameworkWebModule));

//添加autofac模块,需要添加模块
builder.Host.ConfigureAutoFacContainer(container =>
{
    container.RegisterYiModule(AutoFacModuleEnum.PropertiesAutowiredModule, typeof(YiFrameworkWebModule).Assembly);
});


var app = builder.Build();


var t = app.Services.GetService<Test2Entity>();
app.MapControllers();
app.Run();
