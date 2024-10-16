using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Settings;

namespace Yi.Abp.Domain.Shared.Settings
{
    /// <summary>
    /// 数字藏品配置
    /// </summary>
    internal class DigitalCollectiblesSettingProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                //每日矿池最大上限
                new SettingDefinition("MaxPoolLimit", "100"),
                
                //每日手动挖矿最大上限
                new SettingDefinition("MiningMaxLimit", "10"),
                
                //每次挖矿最小间隔（秒）
                new SettingDefinition("MiningMinIntervalSeconds", "5")
            );
        }
    }
}