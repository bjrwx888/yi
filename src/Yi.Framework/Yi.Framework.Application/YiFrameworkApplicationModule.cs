
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;
using Yi.Framework.Application.Contracts;
using Yi.Framework.Application.Contracts.Student;
using Yi.Framework.Application.Student;
using Yi.Framework.Domain.Shared;

namespace Yi.Framework.Application
{
    [DependsOn(
        typeof(YiFrameworkDomainSharedModule),
       typeof(YiFrameworkApplicationContractsModule) )]
    public class YiFrameworkApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IStudentService, StudentService>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

        }
    }
}
