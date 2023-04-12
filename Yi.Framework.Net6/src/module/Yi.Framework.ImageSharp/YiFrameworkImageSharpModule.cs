﻿using Hei.Captcha;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.ImageSharp
{
    public class YiFrameworkImageSharpModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddSingleton<ImageSharpManager>();
            services.AddHeiCaptcha();
        }
    }
}