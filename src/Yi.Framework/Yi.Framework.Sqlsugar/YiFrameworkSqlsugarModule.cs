using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;
using Yi.Framework.Domain;
using Yi.Framework.Domain.Student.IRepository;
using Yi.Framework.Sqlsugar.Student;

namespace Yi.Framework.Sqlsugar
{
    [DependsOn(typeof(YiFrameworkDomainModule))]
    public class YiFrameworkSqlsugarModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IStudentRepository, StudentRepository>();
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

        }


    }
}
