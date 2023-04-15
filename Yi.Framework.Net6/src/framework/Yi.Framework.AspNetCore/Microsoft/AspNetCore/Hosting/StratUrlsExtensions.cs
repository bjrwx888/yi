using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Microsoft.AspNetCore.Hosting
{
    public static class StratUrlsExtensions
    {
        public static IWebHostBuilder UseStartUrlsServer(this IWebHostBuilder hostBuilder, IConfiguration configuration, string option = "StartUrl")
        {
            return hostBuilder.UseUrls(configuration.GetValue<string>(option));
        }

    }
}
