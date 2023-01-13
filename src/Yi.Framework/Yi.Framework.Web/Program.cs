
using Yi.Framework.Web;

var builder = WebApplication.CreateBuilder(args);

//添加配置文件
builder.Host.AddAppSettingsSecretsJson();
//添加模块化
await builder.AddApplicationAsync<YiFrameworkWebModule>();

var app = builder.Build();

//使用模块化
await app.InitializeApplicationAsync();
await app.RunAsync();
