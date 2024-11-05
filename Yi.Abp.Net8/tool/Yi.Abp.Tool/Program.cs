using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yi.Abp.Tool;

class Program
{
    static async Task Main(string[] args)
    {

#if DEBUG
        //args = ["v"];
        //args = ["-v"];
        //args = ["h"];
        //args = ["-h"];
        //args = [];
        //args = ["12312"];
        //args = ["new", "Acme.Book", "-t", "module", "-csf"];
        //args = ["new", "Acme.Book", "-t", "module"];
        //args = ["add-module", "Acme.Demo", "-s", "D:\\code\\csharp\\source\\Yi\\Yi.Abp.Net8", "-modulePath", "D:\\code\\csharp\\source\\Yi\\Yi.Abp.Net8\\module\\acme-demo"];
        // args = ["clear", "-path", "D:\\code\\csharp\\source\\Yi\\Yi.Abp.Net8\\src"];
       
        //帮助
        //args = ["-h"];
        
        //版本
        // args = ["-v"];
        
        //清理
        // args = ["clear"];
        
        //创建模块
        args = ["new","oooo", "-t","module","-p","D:\\temp","-csf"];
        
        //添加模块
        //args = ["add-module", "kkk"];
#endif
        try
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(async (host, service) =>
                {
                    await service.AddApplicationAsync<YiAbpToolModule>();
                })
                //.ConfigureAppConfiguration(configurationBuilder =>
                //{
                //    configurationBuilder.AddJsonFile("appsettings.json");
                //})
                .UseAutofac()
                .Build();
            var commandSelector = host.Services.GetRequiredService<CommandInvoker>();
            await commandSelector.InvokerAsync(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }

}