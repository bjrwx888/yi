using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Abp.Tool.HttpApi.Client;

namespace Yi.Abp.Tool
{
    [DependsOn(typeof(YiAbpToolHttpApiClientModule))]
    public class YiAbpToolModule : AbpModule
    {
    }
}
