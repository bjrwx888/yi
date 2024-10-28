﻿using System;
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
                //每日矿池最大上限--控制矿池膨胀率
                new SettingDefinition("MaxPoolLimit", "100"),

                //每日挖矿最大上限--控制无限挖矿
               // new SettingDefinition("MiningMaxLimit", "36"),
               new SettingDefinition("MiningMaxLimit", "999"),
                
                //每次挖矿最小间隔（秒）--控制暴力挖矿
                new SettingDefinition("MiningMinIntervalSeconds", "3"),

                //每次挖到矿的概率--控制爆率
                // new SettingDefinition("MiningMinProbability", "0.06"),
                new SettingDefinition("MiningMinProbability", "0.6"),
                
                //交易税率--控制频繁交易
                new SettingDefinition("MarketTaxRate", "0.02"),
                
                
                
                //矿池刷新内容
                new SettingDefinition("PoolData", "60,24,10,3,1")
                
                
            );
        }
    }
}