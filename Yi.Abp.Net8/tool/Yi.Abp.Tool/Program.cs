using System.Reflection;
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
        args = ["new", "Acme.Book","-t", "module", "-csf"];
#endif
        try
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(async (host, service) =>
                {
                    await service.AddApplicationAsync<YiAbpToolModule>();
                })
                .UseAutofac()
                .Build();

            var commandSelector = host.Services.GetRequiredService<CommandSelector>();
            await commandSelector.SelectorAsync(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
        Console.ReadKey();
    }

}