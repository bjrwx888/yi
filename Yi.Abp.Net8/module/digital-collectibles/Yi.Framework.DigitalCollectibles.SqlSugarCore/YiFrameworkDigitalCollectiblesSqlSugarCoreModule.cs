using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Yi.Framework.DigitalCollectibles.Domain;
using Yi.Framework.Mapster;
using Yi.Framework.SqlSugarCore;
using Yi.Framework.SqlSugarCore.Abstractions;


namespace Yi.Framework.DigitalCollectibles.SqlsugarCore
{
    [DependsOn(
        typeof(YiFrameworkDigitalCollectiblesDomainModule),
        
        typeof(YiFrameworkMapsterModule),
        typeof(YiFrameworkSqlSugarCoreModule)
        )]
    public class YiFrameworkDigitalCollectiblesSqlSugarCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}