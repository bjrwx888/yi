using Microsoft.AspNetCore.Mvc;
using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;

namespace Yi.Framework.Application
{
    [DynamicWebApi]
    public class TestService : IDynamicWebApi
    {
        public string GetShijie()
        {
            return "你好世界";
        }
    }
}