using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yi.Abp.Tool;

class Program
{
    static void Main(string[] args)
    {

        try
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(async (host, service) =>
                {
                    await service.AddApplicationAsync<YiAbpToolModule>();
                })
                .UseAutofac()
                .Build();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }

        if (args.Contains("-v"))
        {
            var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            Console.WriteLine($"Yi-ABP CLI {version}");
        }
        else
        {
            Console.WriteLine("""
                Usage:

                    yi-abp <command> <target> [options]

                Command List:
                """);
        }

    }

}