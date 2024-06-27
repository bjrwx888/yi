using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.Timing;

namespace Yi.Framework.SettingManagement.Application;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpTimingModule)
)]
public class YiFrameworkSettingManagementApplicationModule : AbpModule
{
}
