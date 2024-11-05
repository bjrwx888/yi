using Microsoft.Extensions.CommandLineUtils;

namespace Yi.Abp.Tool.Commands;

public class TestCommand:ICommand
{
    public string Command => "clear";

    public Task CommandLineApplicationAsync(CommandLineApplication application)
    {
       var sss= application.Option("-i| --id|-l <ID>","内容id",CommandOptionType.SingleValue);
        
        application.OnExecute(() =>
        {
            Console.WriteLine($"你好,---{sss.Value()}");
            return 0;
        });
        return Task.CompletedTask;
    }
}