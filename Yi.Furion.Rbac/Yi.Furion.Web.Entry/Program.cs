using Yi.Framework.Infrastructure.AspNetCore;

Serve.Run(RunOptions.Default.WithArgs(args)
.ConfigureBuilder(x =>
{
    x.WebHost.UseStartUrlsServer(x.Configuration);
}
));
