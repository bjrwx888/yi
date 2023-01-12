using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StartupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Domain.Student.IRepository;
using Yi.Framework.Sqlsugar.Student;

namespace Yi.Framework.Sqlsugar
{
    public class YiFrameworkSqlsugarModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
        }
    }
}
