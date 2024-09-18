using Volo.Abp.Modularity;
using Yi.Framework.SqlSugarCore;

namespace Yi.Framework.GoView.SqlSugarCore
{
    [DependsOn(typeof(YiFrameworkSqlSugarCoreModule))]
    public class YiFrameworkGoViewSqlSugarCoreModule : AbpModule
    {
    }
}
