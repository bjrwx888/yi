using Volo.Abp.Application.Services;

namespace Yi.Abp.Application
{
    public class TestService : ApplicationService
    {
        /// <summary>
        /// 你好世界
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetHelloWorld(string? name)
        {
            return name ?? "HelloWord";
        }
    }
}
