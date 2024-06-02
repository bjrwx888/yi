using Yi.Abp.Tool.HttpApi.Client;

namespace Yi.Abp.Tool
{
    [DependsOn(typeof(YiAbpToolHttpApiClientModule)
        )]
    public class YiAbpToolModule : AbpModule
    {
    }
}
