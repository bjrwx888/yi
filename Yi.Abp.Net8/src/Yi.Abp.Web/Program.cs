using Yi.Abp.Web;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(builder.Configuration["App:SelfUrl"]);
builder.Host.UseAutofac();
await builder.Services.AddApplicationAsync<YiAbpWebModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();
await app.RunAsync();
