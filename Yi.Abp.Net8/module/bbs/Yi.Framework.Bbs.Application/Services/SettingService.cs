using Volo.Abp.Application.Services;
using Yi.Framework.Bbs.Application.Contracts.IServices;

namespace Yi.Framework.Bbs.Application.Services
{
    /// <summary>
    /// Setting服务实现
    /// </summary>
    public class SettingService : ApplicationService,
       ISettingService
    {
        /// <summary>
        /// 获取配置标题
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> GetTitleAsync()
        {
            return Task.FromResult("你好世界");
        }
    }
}
