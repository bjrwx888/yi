
using AspNetCore.Microsoft.AspNetCore.Builder;
using System.Reflection;
using Yi.Framework.Application;
using Yi.Framework.Application.Contracts;
using Yi.Framework.Autofac.Extensions;
using Yi.Framework.Core;
using Yi.Framework.Core.AutoMapper;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Core.Sqlsugar;
using Yi.Framework.Ddd;
using Yi.Framework.Domain;
using Yi.Framework.Domain.Shared;
using Yi.Framework.Sqlsugar;
using Yi.Framework.Web;

//这里是最早执行的地方
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

//使用autofac
builder.Host.UseAutoFacServerProviderFactory();

var app = builder.Build();
app.MapControllers();
app.Run();
