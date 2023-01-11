
using AspNetCore.Microsoft.AspNetCore.Builder;
using Panda.DynamicWebApi;
using Yi.Framework.Application;
using Yi.Framework.Core;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Core.Sqlsugar;
using Yi.Framework.Core.Sqlsugar.Repository;
using Yi.Framework.Ddd;
using Yi.Framework.Ddd.Repository;
using Yi.Framework.Web;

var builder = WebApplication.CreateBuilder(args);

builder.UseYiModules(
    typeof(YiFrameworkCoreModule).Assembly,
    typeof(YiFrameworkDddModule).Assembly,
    typeof(YiFrameworkCoreSqlsugarModule).Assembly
    );
builder.Services.AddControllers();
builder.Services.AddDynamicWebApi();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerServer<YiFrameworkApplicationModule>();
//builder.Services.AddAutoIocServer("Yi.Framework.Core.Sqlsugar");


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
