
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp;
using Yi.Framework.Domain.Student;
using Yi.Framework.Domain.Shared;

namespace Yi.Framework.Domain
{
    [DependsOn(typeof(YiFrameworkDomainSharedModule))]
    public class YiFrameworkDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<StudentManager>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
        
        }
    }
}
