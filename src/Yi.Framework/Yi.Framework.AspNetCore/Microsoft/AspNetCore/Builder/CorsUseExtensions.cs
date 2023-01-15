using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Microsoft.AspNetCore.Builder
{
    public static class CorsUseExtensions
    {
        public static void UseCorsServer(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
        }

    }
}
