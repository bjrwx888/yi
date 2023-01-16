
using AspNetCore.Microsoft.AspNetCore.Builder;
using Autofac;
using System.Reflection;
using Yi.Framework.Application;
using Yi.Framework.Application.Contracts;
using Yi.Framework.Core;
using Yi.Framework.Core.Autofac.Extensions;
using Yi.Framework.Core.Autofac.Modules;
using Yi.Framework.Core.AutoMapper;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Core.Sqlsugar;
using Yi.Framework.Ddd;
using Yi.Framework.Domain;
using Yi.Framework.Domain.Shared;
using Yi.Framework.Sqlsugar;
using Yi.Framework.Web;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls(builder.Configuration.GetValue<string>("StartUrl"));

//添加模块
builder.UseYiModules(
    typeof(YiFrameworkCoreModule).Assembly,
    typeof(YiFrameworkCoreAutoMapperModule).Assembly,
    typeof(YiFrameworkDddModule).Assembly,
    typeof(YiFrameworkCoreSqlsugarModule).Assembly,

     typeof(YiFrameworkSqlsugarModule).Assembly,
     typeof(YiFrameworkDomainSharedModule).Assembly,
     typeof(YiFrameworkDomainModule).Assembly,
     typeof(YiFrameworkApplicationContractsModule).Assembly,
     typeof(YiFrameworkApplicationModule).Assembly,
     typeof(YiFrameworkWebModule).Assembly

    );


//添加autofac模块,需要添加模块
builder.Host.ConfigureAutoFacContainer(container =>
{
    container.RegisterYiModule(AutoFacModuleEnum.PropertiesAutowiredModule, typeof(YiFrameworkWebModule).Assembly);
});


var app = builder.Build();


var t = app.Services.GetService<Test2Entity>();
app.MapControllers();
app.Run();
