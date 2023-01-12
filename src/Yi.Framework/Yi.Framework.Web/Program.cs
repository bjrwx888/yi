
using AspNetCore.Microsoft.AspNetCore.Builder;
using Panda.DynamicWebApi;
using System.Reflection;
using Yi.Framework.Application;
using Yi.Framework.Application.Contracts;
using Yi.Framework.Autofac.Extensions;
using Yi.Framework.Core;
using Yi.Framework.Core.AutoMapper;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Core.Sqlsugar;
using Yi.Framework.Core.Sqlsugar.Repository;
using Yi.Framework.Ddd;
using Yi.Framework.Ddd.Repository;
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
     typeof(YiFrameworkApplicationModule).Assembly
    );

//使用autofac
builder.Host.UseAutoFacServerProviderFactory();

//添加控制器与动态api
builder.Services.AddControllers();
builder.Services.AddDynamicWebApi();

//添加swagger
builder.Services.AddSwaggerServer<YiFrameworkApplicationModule>();


var app = builder.Build();

app.Services.GetRequiredService<IRepository<TestEntity>>();
//if (app.Environment.IsDevelopment())
{
    app.UseSwaggerServer();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.Run();
